using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class DoorBehaviourScript : Receiver {
    public bool open;
    public Vector3 target,home;


    protected void ToggleOpen(){
        open = !open;
    }

    protected override void ColliderEnter(){
        ToggleOpen();
    }

    protected override void ColliderExit(){
        ToggleOpen();
    }

    protected void init()
    {
        open = false;
        target = transform.position;
        home = transform.position;
    }
}
