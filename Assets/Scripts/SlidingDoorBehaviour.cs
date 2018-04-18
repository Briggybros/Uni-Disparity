using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SlidingDoorBehaviour : DoorBehaviourScript {
    public float OpenHeight;
	public int DownSpeed;
	public int UpSpeed;
    public bool looping;
	public bool blocked;
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

	[Command]
	void CmdsyncChange() {
		RpcupdateState();
	}

	[ClientRpc]
	void RpcupdateState() {
		blocked = true;
		open = true;
	}


	[Command]
	void CmdsyncRevert() {
		RpcupdateRevert();
	}

	[ClientRpc]
	void RpcupdateRevert() {
		blocked = false;
		open = false;
	}


	protected override void ColliderWithin(){
		Debug.Log("sdafsd");
		CmdsyncChange();
     }

     protected override void ColliderExit(){
		CmdsyncRevert();
         
     }
}
