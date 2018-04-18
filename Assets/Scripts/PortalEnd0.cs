using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PortalEnd0 : Trigger {

	public GameObject end;
	public int count;
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
		if(count < 0){
			count = 0;
		}
		base.Update();
	}

	protected virtual void OnTriggerEnter(Collider other) {
		if (other.tag == "Player" && this.gameObject != null) {
			other.gameObject.BroadcastMessage("IncCount",this.gameObject);
		}
	}

	protected virtual void OnTriggerExit(Collider other) {
		if (other.tag == "Player" && this.gameObject != null) {
			other.gameObject.BroadcastMessage("DecCount",this.gameObject);
		}
	}
}
