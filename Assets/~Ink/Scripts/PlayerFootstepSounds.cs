using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFootstepSounds : MonoBehaviour {
    public CharacterController PlayerController;
    public float FootstepVolume = 1;
    public float FootstepVolumeV;
    public float CurrentFootstepVolume = 1;
    public float Smooth = 0.1f;
    public AudioClip[] FootstepClips;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame

    //This smoothly creates a realistic volume effect for the footsteps to become louder when sprinting and also plays the animation while doing so.
	void LateUpdate ()
    {
        CurrentFootstepVolume = Mathf.SmoothDamp(CurrentFootstepVolume, FootstepVolume, ref FootstepVolumeV, Smooth);

        gameObject.GetComponent<AudioSource>().volume = CurrentFootstepVolume;

        if (PlayerController.isGrounded == true)
        {
            if (Input.GetAxis("Horizontal") > 0 || Input.GetAxis("Horizontal") < 0 || Input.GetAxis("Vertical") > 0 || Input.GetAxis("Vertical") < 0)
            {
                gameObject.GetComponent<Animator>().SetBool("Run", true);
            }
            else
            {
                gameObject.GetComponent<Animator>().SetBool("Run", false);
            }
        }
        else
        {
            gameObject.GetComponent<Animator>().SetBool("Run", false);
        }
	}

    //Footstep clips that are randomly played after each other's length on an animation event.
    void FootstepSound()
    {
        if (Input.GetKey("left shift"))
        {
            FootstepVolume = 1;
            gameObject.GetComponent<Animator>().speed = 1.2f;
        }
        else
        {
            FootstepVolume = 0.5f;
            gameObject.GetComponent<Animator>().speed = 1;
        }
        gameObject.GetComponent<AudioSource>().pitch = Random.Range(1, 1.3f);
        gameObject.GetComponent<AudioSource>().clip = FootstepClips[Random.Range(0, FootstepClips.Length)];
        gameObject.GetComponent<AudioSource>().Play();
    }
}
