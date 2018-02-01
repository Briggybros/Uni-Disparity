using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MyNetworkManager : NetworkManager {

	private const string TARGET_NAME = "ImageTarget" ;

	public override void OnServerAddPlayer(NetworkConnection connection, short playerControllerId) {
		SpawnOnTargetDetect spawn = GameObject.Find(TARGET_NAME).GetComponent<SpawnOnTargetDetect>();
		spawn.OnServerAddPlayer(connection, playerControllerId);
	}
}
