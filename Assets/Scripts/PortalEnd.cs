using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PortalEnd : Trigger {

	public bool isTransition;
	public GameObject loadingPanel;
	public AudioClip loadingAudio;
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
			if (isTransition) {
				loadingPanel.SetActive(true);
				GameObject.Find("FXSource").GetComponent<AudioSource>().PlayOneShot(loadingAudio);
				NetworkManager.singleton.ServerChangeScene("Level 1");
			} else {
				end.GetComponent<ScoreboardController>().EndGame();
			}
			count = 0;
		}
		if(count < 0){
			count = 0;
		}
		base.Update();
	}

	protected virtual void OnTriggerEnter(Collider other) {
		if (other.CompareTag("Player") && this.gameObject != null) {
			other.gameObject.BroadcastMessage("IncCount",this.gameObject);
		}
	}

	protected virtual void OnTriggerExit(Collider other) {
		if (other.CompareTag("Player") && this.gameObject != null) {
			other.gameObject.BroadcastMessage("DecCount",this.gameObject);
		}
	}
}
