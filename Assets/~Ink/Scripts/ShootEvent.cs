using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ShootEvent : NetworkBehaviour {
    public GameObject Player;
    public Transform Bullet_Transform;
    public GameObject Bullet;
    public GameObject GunSound;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    //Shoots a bullet over the network for players to see.
    [Command]
    void CmdShoot()
    {
        GameObject GunSoundObject = Instantiate(GunSound, gameObject.GetComponent<Transform>().position, gameObject.GetComponent<Transform>().rotation);
        GunSoundObject.GetComponent<Transform>().parent = Player.GetComponent<Transform>();
        GunSoundObject.GetComponent<AudioSource>().pitch = Random.Range(1, 1.3f);
        GunSoundObject.GetComponent<AudioSource>().Play();
        Player.GetComponent<PlayerMovement>().ShootGun = true;
        Player.GetComponent<Animator>().SetBool("Shoot", true);
        GameObject.Find("CROSSHAIR_HUD").GetComponent<AllCrosshairAnimation>().CrosshairShootUp.Play("ShootUp");
        GameObject.Find("CROSSHAIR_HUD").GetComponent<AllCrosshairAnimation>().CrosshairShootDown.Play("ShootDown");
        GameObject.Find("CROSSHAIR_HUD").GetComponent<AllCrosshairAnimation>().CrosshairShootLeft.Play("ShootLeft");
        GameObject.Find("CROSSHAIR_HUD").GetComponent<AllCrosshairAnimation>().CrosshairShootRight.Play("ShootRight");
        GameObject BulletClone = Instantiate(Bullet, Bullet_Transform.position, Bullet_Transform.rotation);
        NetworkServer.Spawn(BulletClone);
        BulletClone.GetComponent<Rigidbody>().AddForce(Bullet_Transform.TransformDirection(Vector3.forward) * 2300);
    }
}
