using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class forcePos : MonoBehaviour {
	public float x, y, z;
	private Vector3 forcePosVec;
	// Use this for initialization
	void Start () {
		forcePosVec = new Vector3(x, y, z);
		transform.localPosition = forcePosVec;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
