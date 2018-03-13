using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecatorCameraController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.RotateAround(Vector3.zero, Vector3.up, 20 * Time.deltaTime);
		transform.LookAt(Vector3.zero);
	}
}
