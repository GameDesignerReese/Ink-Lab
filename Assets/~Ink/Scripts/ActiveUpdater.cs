using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveUpdater : MonoBehaviour {
    public GameObject[] Buttons;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        //For some reason, when the buttons are related to the network manager events, it set's itself inactive, this just sets it back to active
        Buttons[0].SetActive(true);
        Buttons[1].SetActive(true);
        Buttons[2].SetActive(true);
    }
}
