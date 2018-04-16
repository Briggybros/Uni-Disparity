using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InheritSingleRot : MonoBehaviour {

	public GameObject parent;
	public GameObject actualParent;
	public bool x;
	public bool y;
	public bool z;
	
	// Update is called once per frame
	void Update () {
		if (CompareTag("Bopped")) {
			Vector3 angles = new Vector3(1, 1, 1);
			angles.x = this.transform.localEulerAngles.x;
			angles.y = this.transform.localEulerAngles.y;
			angles.z = this.transform.localEulerAngles.z;
			if (x) {
				angles.x = parent.transform.localEulerAngles.z;
			}
			if (y) {
				angles.y = parent.transform.localEulerAngles.y + 90;
			}
			if (z) {
				angles.z = parent.transform.localEulerAngles.x * -1;
			}
			this.transform.localRotation = Quaternion.Euler(angles.x, angles.y, angles.z);
		}
	}

	protected void SwitchReceived() {
		this.tag = "Bopped";
	}
}
