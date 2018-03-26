using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameContainer : MonoBehaviour {
    //This is a container for other scripts to access.

    //When using another script
    //Destroy(GameObject.Find("INTRO_HUD")) doesn't work
    //Destroy(GameObject.Find("GAME_CONTAINER")).GetComponent<GameContainer>().INTRO_HUD) Does work
    public GameObject MAIN_HUD;
    public GameObject INTRO_HUD;
    public GameObject INTRO_CAMERA;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
