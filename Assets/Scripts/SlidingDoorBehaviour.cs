using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SlidingDoorBehaviour : DoorBehaviourScript {
    public float OpenHeight;
	public int DownSpeed;
	public int UpSpeed;
    public bool looping;
	public bool mirror;

	[SyncVar]
	public bool blocked;

	public GameObject[] players;
	public GameObject heldPlayer;
	// Use this for initialization
	protected void Start () {
        init();
        target.y += OpenHeight;
        if (looping) 
        {
            Looping();
        }
		players = GameObject.FindGameObjectsWithTag("Player");
		foreach (GameObject player in players) {
			/*i/*f (mirror) {
				if (!(player.GetComponent<JoystickCharacter>().isLocalPlayer)) {
					heldPlayer = player;
				}	
			}
			else {*/
					if (player.GetComponent<JoystickCharacter>().isLocalPlayer) {
					heldPlayer = player;
			//	}	
			}
		}

    }
	
	// Update is called once per frame
	protected override void Update () {
		if(heldPlayer = null){
			players = GameObject.FindGameObjectsWithTag("Player");
		foreach (GameObject player in players) {
			/*i/*f (mirror) {
				if (!(player.GetComponent<JoystickCharacter>().isLocalPlayer)) {
					heldPlayer = player;
				}	
			}
			else {*/
					if (player.GetComponent<JoystickCharacter>().isLocalPlayer) {
					heldPlayer = player;
			//	}	
			}
		}
		}
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

	protected override void ColliderWithin(){
		Debug.Log("sdafsd");
		//blocked = true;
		//open = true;
	//	heldPlayer.BroadcastMessage("CmdForceOwnership");
		heldPlayer.BroadcastMessage("Block",gameObject);
	//	heldPlayer.BroadcastMessage("CmdRevokeOwnership");
	}

     protected override void ColliderExit(){
	//	heldPlayer.BroadcastMessage("CmdForceOwnership");
		heldPlayer.BroadcastMessage("Unblock",gameObject);
		//heldPlayer.BroadcastMessage("CmdRevokeOwnership");
		//blocked = false;
		//open = false;
         
     }

	protected override void ColliderEnter() {
	}
}
