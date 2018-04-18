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
	public GameObject[] players;
	public GameObject heldPlayer;
	// Use this for initialization
	protected override void Start () {
        init();
        target.y += OpenHeight;
        if (looping) 
        {
            Looping();
        }
		players = GameObject.FindGameObjectsWithTag("Player");
		foreach (GameObject player in players) {
			if (player.GetComponent<JoystickCharacter>().isLocalPlayer) {
				heldPlayer = player;
			}
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
	void CmdsyncChange(GameObject heldplayer) {
		gameObject.GetComponent<NetworkIdentity>().AssignClientAuthority(heldplayer.GetComponent<NetworkIdentity>().connectionToClient);
		RpcupdateState();
		gameObject.GetComponent<NetworkIdentity>().RemoveClientAuthority(heldplayer.GetComponent<NetworkIdentity>().connectionToClient);
	}

	[ClientRpc]
	void RpcupdateState() {
		blocked = true;
		open = true;
	}


	[Command]
	void CmdsyncRevert(GameObject heldplayer) {
		gameObject.GetComponent<NetworkIdentity>().AssignClientAuthority(heldplayer.GetComponent<NetworkIdentity>().connectionToClient);
		RpcupdateRevert();
		gameObject.GetComponent<NetworkIdentity>().RemoveClientAuthority(heldplayer.GetComponent<NetworkIdentity>().connectionToClient);
	}

	[ClientRpc]
	void RpcupdateRevert() {
		blocked = false;
		open = false;
	}


	protected override void ColliderWithin(){
		Debug.Log("sdafsd");
		CmdsyncChange(heldPlayer);
     }

     protected override void ColliderExit(){
		CmdsyncRevert(heldPlayer);
         
     }

	protected override void ColliderEnter() {
	}
}
