using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ABall : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision c) {
        if(c.transform.name == "Spinner") {
            transform.parent = c.transform;
        }
    }
}
