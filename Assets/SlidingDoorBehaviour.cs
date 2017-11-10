using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingDoorBehaviour : DoorBehaviourScript {
    public int OpenHeight;
    // Use this for initialization
    protected override void Start () {
        init();
        target.y += OpenHeight;
    }
	
	// Update is called once per frame
	protected override void Update () {
        if (open)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, 4 * Time.deltaTime);
            //Open the door
        }
        else
        {
            //do whatever doors do while they wait
            transform.position = Vector3.MoveTowards(transform.position, home, 4 * Time.deltaTime);
        }
    }
}
