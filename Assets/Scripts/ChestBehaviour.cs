using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestBehaviour : MonoBehaviour {

	public GameObject key;

	// Update is called once per frame
	void Update () {
		if (this.tag == "Bopped") {
			base.gameObject.BroadcastMessage("PulseReceived");
			this.tag = "Static";
			if(this.transform.GetChild(0) != null) {
				GameObject child = this.transform.GetChild(0).gameObject;
				child.transform.SetParent(null);
				child.gameObject.tag = "Bopped";
			}
		}
	}
}
