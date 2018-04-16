using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfView : MonoBehaviour {
	public bool vis;
	void OnBecameVisible() {
		vis = true;
	}
	void OnBecameInvisible() {
		vis = false;
	}
}
