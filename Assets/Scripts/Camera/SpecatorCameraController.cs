using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecatorCameraController : MonoBehaviour {
	private const int ROTATEWORLD = 1;
	private const int FOLLOW = 2;
	private int camera = ROTATEWORLD;
	public char world;

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		if (camera == ROTATEWORLD){
			transform.RotateAround(Vector3.zero, Vector3.up, 20 * Time.deltaTime);
			transform.LookAt(Vector3.zero);
		}
	}
}
