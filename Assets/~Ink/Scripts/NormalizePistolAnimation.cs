using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalizePistolAnimation : MonoBehaviour {
    public Animation PistolShooter;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void Normalize()
    {
        //When the gameobject is inactive, we need to prevent a frozen shoot animation when it's back to active.
        PistolShooter.Play("PistolNormal");
    }
}
