using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulseTrigger : CollisionTrigger{


    protected override void OnTriggerStay(Collider other) {
        if ((other.gameObject == owner) && (Input.GetKeyDown(KeyCode.E))) {
            foreach (GameObject target in base.targets) {
                target.gameObject.GetComponent<ListenerScript>().BroadcastMessage("PulseFlag");
            }
        }
    }
}
