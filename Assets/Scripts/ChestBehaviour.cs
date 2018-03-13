using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestBehaviour : MonoBehaviour {

	public GameObject key;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (this.tag == "Bopped") {
			base.gameObject.BroadcastMessage("PulseReceived");
			this.tag = "Static";
		}
	}
}
