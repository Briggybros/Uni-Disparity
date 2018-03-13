using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecatorCameraController : MonoBehaviour {
	private const int ROTATEWORLD = 1;
	private const int FOLLOW = 2;
	private new int camera = ROTATEWORLD;
	public char world;

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void LateUpdate () {
		if (camera == ROTATEWORLD){
			transform.LookAt(Vector3.zero);
		}
	}
}
