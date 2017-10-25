using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorCollisionTrigger: CollisionTrigger {
    
	
    // Update is called once per frame
    void Update () {
        if (active){
            TriggerDoor();
            active = false;
        }
    }

    void TriggerDoor()
    {
        target.GetComponent<DoorBehaviourScript>().ToggleOpen(true);
    }
}
