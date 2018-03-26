using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HideDeathScreen : MonoBehaviour {
    public Image[] DeathScreens;
    public Text DeathText;
    public bool Hidden;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        //Instead of inactivating this object though another script which can cause problems, this instead inactivates the images and text.
        if (Hidden == true)
        {
            DeathScreens[0].enabled = false;
            DeathScreens[1].enabled = false;
            DeathText.enabled = false;
        }
        else
        {
            DeathScreens[0].enabled = true;
            DeathScreens[1].enabled = true;
            DeathText.enabled = true;
        }
	}
}
