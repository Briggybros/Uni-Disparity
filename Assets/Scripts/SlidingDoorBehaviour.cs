﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingDoorBehaviour : DoorBehaviourScript {
    public float OpenHeight;
	public int DownSpeed;
	public int UpSpeed;
    public bool looping;
	// Use this for initialization
	protected override void Start () {
        init();
        target.y += OpenHeight;
        if (looping) 
        {
            Looping();
        }
    }
	
	// Update is called once per frame
	protected override void Update () {
        if (open)
        {   
            //Open the door
            Debug.Log("opening");
            transform.position = Vector3.MoveTowards(transform.position, target, DownSpeed * Time.deltaTime);
        }
        else
        {   
            //Close the door
            Debug.Log("closing");
            transform.position = Vector3.MoveTowards(transform.position, home, UpSpeed * Time.deltaTime);
        }
    
    }
}
