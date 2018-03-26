using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PlayerMovement : NetworkBehaviour {
    public Animation Pistol_Pivot;
    public Animator CROSSHAIR_HUD;
    public Transform CameraPivot;
    public Transform Crouch_Pivot;
    public Transform Crouch_Transform;
    public Transform NoCrouch_Transform;
    public float XRotation;
    public float YRotation;
    public float XRotationV;
    public float YRotationV;
    public float CurrentXRotation;
    public float CurrentYRotation;
    public Vector3 movDir;
    public float Speed = 3.4f;
    public float SprintSpeed = 6.4f;
    public float CrouchSpeed = 2;
    public float JumpSpeed = 8;
    public float Gravity = 20;
    public float Sensitivity = 0.5f;
    public float Smooth = 0.1f;
    public CharacterController controller;
    public float CrouchHeight = 2;
    public float CrouchHeightV;
    public float CurrentCrouchHeight = 2;
    public Animator GunAnimator;
    public GameObject Pistol_Normal;
    public GameObject Pistol_Sprint;
    public Animator World_Camera;
    public Animator Crosshair_Pivot;
    public float WhichSpawn;
    public GameObject Client_Mesh;
    public GameObject Client_Gun_Mesh;
    public GameObject DeadCameraPivot;
    [SyncVar]public bool Dead;
    [SyncVar]public float DeadTime = 5;
    public GameObject DEATH_HUD;
    public GameObject RESPAWN_TIME;
    [SyncVar]public GameObject ThisPlayer;
    public GameObject ThisLocalPlayer;
    public Transform RaycastKiller;
    public bool ShootGun;
    public GameObject SPAWN_SYSTEM;
    [SyncVar] public float CurrentTags;
    [SyncVar] public bool Killed;
    public GameObject MAIN_HUD;
    public bool HideCursor = true;

    // Use this for initialization
    void Start ()
    {
        //When the player is spawned, we now know that it's necessary to set the main hud active so it can use it's script to set the unnecessary scripts inactive.
        GameObject.Find("GAME_CONTAINER").GetComponent<GameContainer>().MAIN_HUD.SetActive(true);

        //Randomly generates a spot for the player to spawn.
        WhichSpawn = Random.Range(0, 6);
        if (WhichSpawn == 0)
        {
            gameObject.GetComponent<Transform>().position = GameObject.Find("SPAWN_SYSTEM").GetComponent<SpawnSystem>().Spawners[0].position;
        }
        if (WhichSpawn == 1)
        {
            gameObject.GetComponent<Transform>().position = GameObject.Find("SPAWN_SYSTEM").GetComponent<SpawnSystem>().Spawners[1].position;
        }
        if (WhichSpawn == 2)
        {
            gameObject.GetComponent<Transform>().position = GameObject.Find("SPAWN_SYSTEM").GetComponent<SpawnSystem>().Spawners[2].position;
        }
        if (WhichSpawn == 3)
        {
            gameObject.GetComponent<Transform>().position = GameObject.Find("SPAWN_SYSTEM").GetComponent<SpawnSystem>().Spawners[3].position;
        }
        if (WhichSpawn == 4)
        {
            gameObject.GetComponent<Transform>().position = GameObject.Find("SPAWN_SYSTEM").GetComponent<SpawnSystem>().Spawners[4].position;
        }
        if (WhichSpawn == 5)
        {
            gameObject.GetComponent<Transform>().position = GameObject.Find("SPAWN_SYSTEM").GetComponent<SpawnSystem>().Spawners[5].position;
        }
        if (WhichSpawn == 6)
        {
            gameObject.GetComponent<Transform>().position = GameObject.Find("SPAWN_SYSTEM").GetComponent<SpawnSystem>().Spawners[6].position;
        }

        //These are variables that are assigned by Unity's finding system when the player is spawned.
        RESPAWN_TIME = GameObject.Find("RESPAWN_TIME");
        Crosshair_Pivot = GameObject.Find("Crosshair_Pivot").GetComponent<Animator>();
        DEATH_HUD = GameObject.Find("DEATH_HUD");
        CROSSHAIR_HUD = GameObject.Find("CROSSHAIR_HUD").GetComponent<Animator>();
        CROSSHAIR_HUD.GetComponent<CrosshairHide>().Hidden = false;
        controller = gameObject.GetComponent<CharacterController>();
        SPAWN_SYSTEM = GameObject.Find("SPAWN_SYSTEM");
    }

    // Update is called once per frame

    //This brings a networked player back to life by synced variables.
    [Command]
    void CmdBackToLife()
    {
        ThisPlayer = null;
        Dead = false;
    }

    void Update ()
    {
        //locks and hides the cursor.
        if (HideCursor == true)
        {
            if (Input.GetKeyDown("x"))
            {
                HideCursor = false;
            }
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            if (Input.GetKeyDown("x"))
            {
                HideCursor = true;
            }
            Cursor.lockState = CursorLockMode.None;
        }

        //Simple exit of game.
        if (Input.GetKeyDown("escape"))
        {
            Application.Quit();
        }

        //Inactivates the collider on death
        if(Dead == true)
        {
            gameObject.GetComponent<CharacterController>().enabled = false;
        }
        else
        {
            gameObject.GetComponent<CharacterController>().enabled = true;
        }

        //These are commands that are only visualized from the user's computer but also sends synced variables across the network.
        if (isLocalPlayer == true)
        {
            //This is command starts from an event of another script which is connected to the shooting animation.
            if (ShootGun == true)
            {
                LocalKillPlayer();
                CmdKillPlayer();
                ShootGun = false;
            }

            //If the player is dead, the death hud will appear including a timed spawner which spawns the player randomly
            if (Dead == false)
            {
                DeadTime = 5;

                DEATH_HUD.GetComponent<HideDeathScreen>().Hidden = true;
                CROSSHAIR_HUD.GetComponent<CrosshairHide>().Hidden = false;
                DeadCameraPivot.SetActive(false);
                CameraPivot.gameObject.SetActive(true);
            }
            else
            {
                DEATH_HUD.GetComponent<HideDeathScreen>().Hidden = false;
                CROSSHAIR_HUD.GetComponent<CrosshairHide>().Hidden = true;
                DeadCameraPivot.SetActive(true);
                CameraPivot.gameObject.SetActive(false);

                RESPAWN_TIME.GetComponent<Text>().text = "Respawning in " + DeadTime.ToString("F0") + " seconds";
                if (DeadTime > 0)
                {
                    DeadTime -= Time.deltaTime * 1;
                }
                else
                {
                    WhichSpawn = Random.Range(0, 6);
                    if (WhichSpawn == 0)
                    {
                        gameObject.GetComponent<Transform>().position = GameObject.Find("SPAWN_SYSTEM").GetComponent<SpawnSystem>().Spawners[0].position;
                    }
                    if (WhichSpawn == 1)
                    {
                        gameObject.GetComponent<Transform>().position = GameObject.Find("SPAWN_SYSTEM").GetComponent<SpawnSystem>().Spawners[1].position;
                    }
                    if (WhichSpawn == 2)
                    {
                        gameObject.GetComponent<Transform>().position = GameObject.Find("SPAWN_SYSTEM").GetComponent<SpawnSystem>().Spawners[2].position;
                    }
                    if (WhichSpawn == 3)
                    {
                        gameObject.GetComponent<Transform>().position = GameObject.Find("SPAWN_SYSTEM").GetComponent<SpawnSystem>().Spawners[3].position;
                    }
                    if (WhichSpawn == 4)
                    {
                        gameObject.GetComponent<Transform>().position = GameObject.Find("SPAWN_SYSTEM").GetComponent<SpawnSystem>().Spawners[4].position;
                    }
                    if (WhichSpawn == 5)
                    {
                        gameObject.GetComponent<Transform>().position = GameObject.Find("SPAWN_SYSTEM").GetComponent<SpawnSystem>().Spawners[5].position;
                    }
                    if (WhichSpawn == 6)
                    {
                        gameObject.GetComponent<Transform>().position = GameObject.Find("SPAWN_SYSTEM").GetComponent<SpawnSystem>().Spawners[6].position;
                    }
                    CmdBackToLife();
                }
            }

            //The 3rd person model of what the non local player sees is inactive since this is the local player.
            Client_Mesh.SetActive(false);
            Client_Gun_Mesh.SetActive(false);

            //Plays the shooting animation
            if (Input.GetKeyDown("mouse 0"))
            {
                Pistol_Pivot.Play("PistolShoot");
            }

            //Basic non network related character movement which sends data to the client
            if (controller.isGrounded == true)
            {
                if (Input.GetAxis("Horizontal") > 0 || Input.GetAxis("Horizontal") < 0 || Input.GetAxis("Vertical") > 0 || Input.GetAxis("Vertical") < 0)
                {
                    if (CROSSHAIR_HUD != null)
                    {
                        CROSSHAIR_HUD.SetBool("Move", true);
                    }
                    gameObject.GetComponent<Animator>().SetBool("Run", true);
                    GunAnimator.SetBool("Run", true);
                }
                else
                {
                    if (CROSSHAIR_HUD != null)
                    {
                        CROSSHAIR_HUD.SetBool("Move", false);
                    }
                    gameObject.GetComponent<Animator>().SetBool("Run", false);
                    GunAnimator.SetBool("Run", false);
                }

                movDir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
                movDir = gameObject.GetComponent<Transform>().TransformDirection(movDir);

                if (Input.GetKey("left shift"))
                {
                    if (CROSSHAIR_HUD != null)
                    {
                        CROSSHAIR_HUD.SetBool("Sprint", true);
                    }
                    if (Crosshair_Pivot != null)
                    {
                        Crosshair_Pivot.SetBool("Sprint", true);
                    }

                    if(Input.GetAxis("Horizontal") > 0 || Input.GetAxis("Horizontal") < 0 || Input.GetAxis("Vertical") > 0 || Input.GetAxis("Vertical") < 0)
                    {
                        gameObject.GetComponent<Animator>().SetBool("Sprint", true);
                        World_Camera.SetBool("Bob", true);
                        Pistol_Normal.SetActive(false);
                        Pistol_Sprint.SetActive(true);
                        movDir *= SprintSpeed;
                    }
                    else
                    {
                        gameObject.GetComponent<Animator>().SetBool("Sprint", false);
                        World_Camera.SetBool("Bob", false);
                        Pistol_Normal.SetActive(true);
                        Pistol_Sprint.SetActive(false);
                        movDir *= Speed;
                    }

                }
                else
                {
                    gameObject.GetComponent<Animator>().SetBool("Sprint", false);
                    if (CROSSHAIR_HUD != null)
                    {
                        CROSSHAIR_HUD.SetBool("Sprint", false);
                    }
                    if (Crosshair_Pivot != null)
                    {
                        Crosshair_Pivot.SetBool("Sprint", false);
                    }
                    World_Camera.SetBool("Bob", false);
                    Pistol_Normal.SetActive(true);
                    Pistol_Sprint.SetActive(false);
                    movDir *= Speed;
                }

                if (Input.GetButtonDown("Jump"))
                {
                    movDir.y = JumpSpeed;
                }
            }
            else
            {
                gameObject.GetComponent<Animator>().SetBool("Run", false);
                GunAnimator.SetBool("Run", false);
            }

            movDir.y -= Gravity * Time.deltaTime;
            if (Dead == false)
            {
                controller.Move(movDir * Time.deltaTime);
            }
        }
        else
        {
            //Plays a death animation when a non local player is dead.
            if(Dead == true)
            {
                gameObject.GetComponent<Animator>().SetBool("Dead", true);
            }
            else
            {
                gameObject.GetComponent<Animator>().SetBool("Dead", false);
            }

            //Displays a visual for local players to see non local players in third person form.
            Client_Mesh.SetActive(true);
            Client_Gun_Mesh.SetActive(true);
            CameraPivot.gameObject.SetActive(false);
        }
	}

    //A network commanded event which can be interactive on the server
    [Command]
    void CmdKillPlayer()
    {
        //When the player shoots another player locally, the raycast is created to detect if a non local player is there.
        //A raycast killer object was needed to prevent the player from killing it self
        RaycastHit hit;

        if (Physics.Raycast(RaycastKiller.position, RaycastKiller.forward, out hit, 10))
        {
            if (hit.transform.gameObject.tag == "Player")
            {
                ThisPlayer = hit.transform.gameObject;
            }
        }
        if (ThisPlayer != null)
        {
            ThisPlayer.GetComponent<PlayerMovement>().Dead = true;
            if(ThisPlayer.GetComponent<PlayerMovement>().Dead == true)
            {
                ThisPlayer = null;
            }
        }
    }

    void LocalKillPlayer()
    {
        //This is the local version of CmdKillPlayer which locally allows a transform to be assigned which could not be done by [Command]
        //It also gives the player points
        RaycastHit hit2;

        if (Physics.Raycast(RaycastKiller.position, RaycastKiller.forward, out hit2, 10))
        {
            if (hit2.transform.gameObject.tag == "Player")
            {
                ThisLocalPlayer = hit2.transform.gameObject;
            }
        }
        if (ThisLocalPlayer != null)
        {
            if (CameraPivot.gameObject.activeSelf == true)
            {
                CurrentTags += 1;
            }
            GameObject.Find("TAG_AMOUNT").GetComponent<Text>().text = "" + CurrentTags;
            GameObject.Find("HITMARKER").GetComponent<Animation>().Play("Hitmark");
            ThisLocalPlayer.GetComponent<PlayerMovement>().Dead = true;
            if (ThisLocalPlayer.GetComponent<PlayerMovement>().Dead == true)
            {
                ThisLocalPlayer = null;
            }
        }
    }


    void LateUpdate()
    {
        //Basic non network related camera movement which sends data to the client
        if (isLocalPlayer == true)
        {
            if (controller.isGrounded == false)
            {
                gameObject.GetComponent<Animator>().SetBool("Air", true);
                if (CROSSHAIR_HUD != null)
                {
                    CROSSHAIR_HUD.SetBool("Sprint", false);
                }
                if (Crosshair_Pivot != null)
                {
                    Crosshair_Pivot.SetBool("Sprint", false);
                }
                World_Camera.SetBool("Bob", false);
                Pistol_Normal.SetActive(true);
                Pistol_Sprint.SetActive(false);
            }
            else
            {
                gameObject.GetComponent<Animator>().SetBool("Air", false);
            }

            gameObject.GetComponent<CharacterController>().height = CurrentCrouchHeight;

            CurrentCrouchHeight = Mathf.SmoothDamp(CurrentCrouchHeight, CrouchHeight, ref CrouchHeightV, Smooth);

            gameObject.GetComponent<Transform>().rotation = Quaternion.Euler(0, CameraPivot.eulerAngles.y, 0);

            CameraPivot.rotation = Quaternion.Euler(CurrentXRotation, CurrentYRotation, 0);

            if (Dead == false)
            {
                XRotation -= Input.GetAxis("Mouse Y") * Sensitivity * 2 * 2;
                YRotation += Input.GetAxis("Mouse X") * Sensitivity * 2 * 2;
            }

            CurrentXRotation = Mathf.SmoothDamp(CurrentXRotation, XRotation, ref XRotationV, Smooth);
            CurrentYRotation = Mathf.SmoothDamp(CurrentYRotation, YRotation, ref YRotationV, Smooth);

            XRotation = Mathf.Clamp(XRotation, -70, 70);
        }
    }
}
