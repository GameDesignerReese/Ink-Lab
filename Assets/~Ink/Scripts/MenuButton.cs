using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class MenuButton : NetworkBehaviour {
    public GameObject MyMenu;
    public GameObject NextMenu;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //These are public functions from the buttons of canvas using functions from the network manager.
    public void HostGameButton()
    {
        NetworkManager.singleton.StartHost();
    }

    public void ClientButton()
    {
        string ipAdress = "localhost";
        NetworkManager.singleton.networkAddress = ipAdress;
        NetworkManager.singleton.StartClient();
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}
