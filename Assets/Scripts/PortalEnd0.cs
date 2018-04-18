using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class PortalEnd0 : Trigger {

	public GameObject end;
    [SyncVar]
	private int count;

    private GameObject localPlayer; 
	// Use this for initialization
	protected override void Start () {
		count = 0;
		base.Start();
	}
	
	// Update is called once per frame
	protected override void Update () {
		if(count > 1) {
            NetworkManager.singleton.ServerChangeScene("Level 1");
			count = 0;
		}
		base.Update();
	}

	protected virtual void OnTriggerEnter(Collider other) {
		if (other.tag == "Player") {
            if (other.gameObject.GetComponent<JoystickCharacter>().isLocalPlayer) {
                localPlayer = other.gameObject;
            }
			count++;
		}
	}

	protected virtual void OnTriggerExit(Collider other) {
		if (other.tag == "Player") {
			count--;
		}
	}
}
