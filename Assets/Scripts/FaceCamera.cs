using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCamera : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.up = Camera.main.transform.position - transform.position;
        transform.forward = -Camera.main.transform.up;
	}
}
