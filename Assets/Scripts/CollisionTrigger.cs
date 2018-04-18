using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionTrigger : Trigger {


    public GameObject owner;
	public bool playerInteract;

    protected override void Start(){
        base.Start();
    }

    protected override void Update(){
        base.Update();
    }
    protected virtual void OnTriggerEnter(Collider other){
        if (other.gameObject == owner || (playerInteract == true && other.CompareTag("Player"))) {
            foreach (GameObject target in base.targets) {
                target.gameObject.GetComponent<ListenerScript>().BroadcastMessage("EnterFlag");
            }
        }
    }

    protected virtual void OnTriggerExit(Collider other){
        if (other.gameObject == owner || (playerInteract == true && other.CompareTag("Player"))) {
            foreach (GameObject target in base.targets) {
                target.gameObject.GetComponent<ListenerScript>().BroadcastMessage("ExitFlag");
            }
        }
    }

    protected virtual void OnTriggerStay(Collider other){
        if (other.gameObject == owner || (playerInteract == true && other.CompareTag("Player"))) {
            foreach (GameObject target in base.targets) {
                target.gameObject.GetComponent<ListenerScript>().BroadcastMessage("WithinFlag");
            }
        }
    }
}
