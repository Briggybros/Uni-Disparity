using System.Collections;
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
            transform.position = Vector3.MoveTowards(transform.position, target, DownSpeed * Time.deltaTime);
        }
        else
        {   
            //Close the door
            transform.position = Vector3.MoveTowards(transform.position, home, UpSpeed * Time.deltaTime);
        }
    
    }

    // protected void ColliderWithin(){
    //     Debug.Log("sdafsd");
    //     blocked = true;
    // }

    // protected override void ColliderExit(){
    //     blocked=false;
    //     ToggleOpen();
    // }

    // protected override void ToggleOpen(){
    //     if (!blocked) {
    //         open = !open;
    //     }
    // }
}
