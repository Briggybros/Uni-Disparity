using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceUpright : MonoBehaviour {
	void Update () {
		transform.localRotation.eulerAngles.Set(0, transform.localRotation.eulerAngles.y, 0);
	}
}
