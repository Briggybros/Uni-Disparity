﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalEnd : Trigger {

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
			end.GetComponent<ScoreboardController>().EndGame();
			count = 0;
		}
		base.Update();
	}

	protected virtual void OnTriggerEnter(Collider other) {
		if (other.tag == "Player") {
			other.gameObject.BroadcastMessage("IncCount",gameObject);
		}
	}

	protected virtual void OnTriggerExit(Collider other) {
		if (other.tag == "Player") {
			other.gameObject.BroadcastMessage("DecCount",gameObject);
		}
	}
}
