using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotSync : MonoBehaviour {

	public GameObject parent;
	
	// Update is called once per frame
	void Update () {
		this.transform.rotation = parent.transform.rotation;
	}
}
