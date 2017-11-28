using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchTrigger: CollisionTrigger {


    protected override void OnTriggerStay(Collider other) {
		if (requiresInteract) {
			if ((other.gameObject == owner || (playerInteract == true && other.tag == "Player")) && (Input.GetKeyDown(KeyCode.E))) {
				foreach (GameObject target in base.targets) {
					target.gameObject.GetComponent<ListenerScript>().BroadcastMessage("SwitchFlag");
				}
			}
		}
	}

    protected override void OnTriggerEnter(Collider other) {
		if (!requiresInteract) {
			if ((other.gameObject == owner || (playerInteract == true && other.tag == "Player"))) {
				foreach (GameObject target in base.targets) {
					target.gameObject.GetComponent<ListenerScript>().BroadcastMessage("SwitchFlag");
				}
			}
		}
    }

    protected override void OnTriggerExit(Collider other) {
		if (!requiresInteract) {
			if ((other.gameObject == owner || (playerInteract == true && other.tag == "Player"))) {
				foreach (GameObject target in base.targets) {
					target.gameObject.GetComponent<ListenerScript>().BroadcastMessage("SwitchFlag");
				}
			}
		}
	}
}
