using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecatorCameraController : MonoBehaviour {
	void LateUpdate () {
		transform.LookAt(Vector3.zero);
	}
}
