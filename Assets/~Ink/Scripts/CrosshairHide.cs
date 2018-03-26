using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrosshairHide : MonoBehaviour {
    public Image[] CrosshairParts;
    public bool Hidden;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        //Instead of inactivating this object though another script which can cause problems, this instead inactivates the images.
		if(Hidden == true)
        {
            CrosshairParts[0].enabled = false;
            CrosshairParts[1].enabled = false;
            CrosshairParts[2].enabled = false;
            CrosshairParts[3].enabled = false;
        }
        else
        {
            CrosshairParts[0].enabled = true;
            CrosshairParts[1].enabled = true;
            CrosshairParts[2].enabled = true;
            CrosshairParts[3].enabled = true;
        }
	}
}
