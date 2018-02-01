using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MyNetworkManager : NetworkManager {

	private char world;

	public void Start () {
		world = GetComponent<CharacterPicker>().GetWorld();
	}

	private const string TARGET_NAME = "ImageTarget" ;

	public override void OnServerAddPlayer(NetworkConnection connection, short playerControllerId) {
		GameObject playerPrefab = NetworkManager.singleton.spawnPrefabs[world == 'A' ? 1 : 0];
		Transform startPos = NetworkManager.singleton.GetStartPosition();
		GameObject player = (GameObject) Instantiate(playerPrefab, startPos.position, Quaternion.identity);
		player.GetComponent<Rigidbody>().isKinematic = true;
		player.transform.SetParent(GameObject.Find(TARGET_NAME).transform);
		NetworkServer.AddPlayerForConnection(connection, player, playerControllerId);
	}
}
