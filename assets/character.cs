using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class character : MonoBehaviour {

	void Start () {          // Take the initial position
	}

	void Update () {
		if(Input.GetKeyDown(KeyCode.A)) {        // Left
			transform.Rotate(Vector3.up, -90, Space.Self);
		}
		if(Input.GetKeyDown(KeyCode.D)) {        // Right
			transform.Rotate(Vector3.up, 90, Space.Self);
		}
		if(Input.GetKeyDown(KeyCode.W)) {        // Up
			transform.Translate(Vector3.forward);
		}
		if(Input.GetKeyDown(KeyCode.S)) {        // Down
			transform.Translate(Vector3.back);
		}

	}

}
