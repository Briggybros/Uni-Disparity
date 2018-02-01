using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwirlRotation : MonoBehaviour {
	// Update is called once per frame
	void Update () {
		transform.Rotate(Vector3.forward * 3);
	}
}
