using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PauseUI : MonoBehaviour {

	// Use this for initialization
	public void OnPause() {
	}

	public void OnResume() {
	}

	public void Disconnect() {
		NetworkManager.singleton.StopHost();
		NetworkManager.singleton.onlineScene = null;
		Time.timeScale = 1;
	}
}
