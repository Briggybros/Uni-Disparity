using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulseTrigger : InteractTrigger {


	protected override void OnTriggerStay(Collider other) {
		if (requiresInteract) {
			if ((other.gameObject == owner || (playerInteract == true && this.tag == "Bopped"))) {
				foreach (GameObject target in base.targets) {
					target.gameObject.GetComponent<ListenerScript>().BroadcastMessage("PulseFlag");
				}
				interacted = false;
				this.tag = "Static";
			}
		} else {
		}
	}

	protected override void OnTriggerEnter(Collider other) {
		if (!requiresInteract) {
			if ((other.gameObject == owner || (playerInteract == true && other.tag == "Player"))) {
				foreach (GameObject target in base.targets) {
					target.gameObject.GetComponent<ListenerScript>().BroadcastMessage("PulseFlag");
				}
			}
		}
	}

	protected override void OnTriggerExit(Collider other) {

	}

	protected void Update(){
		if(this.tag == "Bopped" && !GetComponent<Collider>().enabled){
			foreach (GameObject target in base.targets) {
					target.gameObject.GetComponent<ListenerScript>().BroadcastMessage("PulseFlag");
				}
				interacted = false;
				this.tag = "Static";
		}
	}
}
