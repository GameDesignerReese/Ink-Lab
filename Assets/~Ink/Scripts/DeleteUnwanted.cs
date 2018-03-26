using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteUnwanted : MonoBehaviour {
    public GameObject INTRO_HUD;
    public GameObject INTRO_CAMERA;
    public GameObject BackgroundMusic;
	// Use this for initialization
	void Start () {
        //When the main hud is active, we are then allowed to get rid of the unnecessary huds
        Destroy(INTRO_HUD);
        Destroy(INTRO_CAMERA);
        BackgroundMusic.SetActive(true);

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
