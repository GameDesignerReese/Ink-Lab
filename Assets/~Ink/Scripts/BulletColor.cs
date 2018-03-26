using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BulletColor : NetworkBehaviour {

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        //Rotates the bullet from the z axis
        gameObject.GetComponent<Transform>().Rotate(0, 0, 10);
    }

    //If the object collides to any other object that has a layer of "Default" which is pretty much all objects, it then destroys it self.
    void OnTriggerEnter(Collider Col)
    {
        if(Col.gameObject.layer == LayerMask.NameToLayer("Default"))
        {
            Destroy(gameObject);
        }
    }
}
