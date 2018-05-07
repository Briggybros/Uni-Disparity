using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PauseUI : MonoBehaviour {
	private NetworkDiscovery discovery;

	public void Start() {
		discovery = GameObject.Find("Network Manager").GetComponent<NetworkDiscovery>();
	}

	public void Disconnect() {
		discovery.StopBroadcast();
		NetworkManager.singleton.StopHost();
		NetworkManager.singleton.onlineScene = null;
		Time.timeScale = 1;
	}
}
