using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionTrigger : Trigger {
    public GameObject owner;

    protected override void Start(){
        base.Start();
    }

    protected override void Update(){
        base.Update();
    }
    protected void OnTriggerEnter(Collider other){
        if (other.gameObject == owner/*ensure it the the player name, or the object name*/){
            foreach (GameObject target in base.targets){
                target.gameObject.GetComponent<ListenerScript>().BroadcastMessage("EnterFlag");
            }
        }
    }

    protected void OnTriggerExit(Collider other){
        if (other.gameObject == owner){
            foreach (GameObject target in base.targets)
            {
                target.gameObject.GetComponent<ListenerScript>().BroadcastMessage("ExitFlag");
            }
        }
    }

    protected void OnTriggerStay(Collider other){
        if (other.gameObject == owner){
            foreach (GameObject target in base.targets)
            {
                target.gameObject.GetComponent<ListenerScript>().BroadcastMessage("WithinFlag");
            }
        }
    }
}
