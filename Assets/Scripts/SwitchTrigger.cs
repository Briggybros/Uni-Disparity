using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchTrigger : InteractTrigger {

	private Collider c;

	protected override void Start() {
		c = GetComponent<Collider>();
	}

	protected override void OnTriggerStay(Collider other) {
		if (requiresInteract) {
			if ((other.gameObject == owner || (playerInteract == true && CompareTag("Bopped")))) {
				foreach (GameObject target in base.targets) {
					target.gameObject.GetComponent<ListenerScript>().BroadcastMessage("SwitchFlag");
				}
				interacted = false;
				this.tag = "Static";
			}
		}
	}

	protected override void OnTriggerEnter(Collider other) {
		if (!requiresInteract) {
			if ((other.gameObject == owner || (playerInteract == true && other.CompareTag("Player")))) {
				foreach (GameObject target in base.targets) {
					target.gameObject.GetComponent<ListenerScript>().BroadcastMessage("SwitchFlag");
				}
			}
		}
	}

	protected override void OnTriggerExit(Collider other) {
		if (!requiresInteract) {
			if ((other.gameObject == owner || (playerInteract == true && other.CompareTag("Player")))) {
				foreach (GameObject target in base.targets) {
					target.gameObject.GetComponent<ListenerScript>().BroadcastMessage("SwitchFlag");
				}
			}
		}
	}

	protected override void Update(){
		if(CompareTag("Bopped") && !c.enabled){
			foreach (GameObject target in base.targets) {
					target.gameObject.GetComponent<ListenerScript>().BroadcastMessage("SwitchFlag");
				}
				interacted = false;
				this.tag = "Static";
		}
	}
}