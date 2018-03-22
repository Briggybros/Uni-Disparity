using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransportBehaviour : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
		if(this.name == "transporterCogBolt" || this.name == "transporterBoltCog") {
			this.transform.GetChild(0).gameObject.SetActive(true);
		}
	}
}
