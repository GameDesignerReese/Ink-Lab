﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LoadLevelOnEvent : MonoBehaviour {
    public string LevelName;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void SceneLoading()
    {
        //Loads a level on an animation event
        SceneManager.LoadScene(LevelName);
    }
}