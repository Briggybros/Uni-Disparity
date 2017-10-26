using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBehaviourScript : Receiver {
    public bool open;


    // Use this for initialization
    protected override void Start () {
        open = false;
    }
	
    // Update is called once per frame
    protected override void Update () {
        if (open){
            Debug.Log("Door opened");
            open = false;
            //Open the door
        }
        else{
            //do whatever doors do while they wait
        }
    }

    protected void ToggleOpen(){
        open = !open;
    }

    protected override void ColliderEnter(){
        ToggleOpen();
    }
}
