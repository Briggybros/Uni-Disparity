using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulseTrigger : InteractTrigger {

	private Collider c;

	protected override void Start() {
		c = GetComponent<Collider>();
	}

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

	protected override void Update() {
		if(CompareTag("Bopped") && !c.enabled){
			foreach (GameObject target in base.targets) {
					target.gameObject.GetComponent<ListenerScript>().BroadcastMessage("PulseFlag");
				}
				interacted = false;
				this.tag = "Static";
		}
	}
}
