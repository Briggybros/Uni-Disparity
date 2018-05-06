using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MyNetworkDiscovery : NetworkDiscovery {
	public void Awake() {
		useNetworkManager = true;
	}

	public override void OnReceivedBroadcast(string fromAddress, string data) {
		Debug.Log("henlo");
		MyNetworkManager.singleton.networkAddress = fromAddress;
		MyNetworkManager.singleton.StartClient();
	}
}
