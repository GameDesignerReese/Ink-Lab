using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteTime : MonoBehaviour {
    public float TimeForDeletion = 2;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        
        TimeForDeletion -= Time.deltaTime * 1;

        //Deletes the object when the float variable is under 0.
        if(TimeForDeletion < 0)
        {
            Destroy(gameObject);
        }
	}

}
