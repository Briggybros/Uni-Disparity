using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingDoorBehaviour : DoorBehaviourScript {
    public int OpenHeight;
	public int DownSpeed;
	public int UpSpeed;
	// Use this for initialization
	protected override void Start () {
        init();
        target.y += OpenHeight;
    }
	
	// Update is called once per frame
	protected override void Update () {
        if (open)
        {
			//Open the door
			transform.position = Vector3.MoveTowards(transform.position, target, DownSpeed * Time.deltaTime);
        }
        else
        {
            //Close the door
            transform.position = Vector3.MoveTowards(transform.position, home, UpSpeed * Time.deltaTime);
        }
    }
}
