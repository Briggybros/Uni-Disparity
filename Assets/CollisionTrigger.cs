using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionTrigger : Trigger {
    public GameObject owner;


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == owner/*ensure it the the player name, or the object name*/){
            Activate(true);
        }
    }
}
