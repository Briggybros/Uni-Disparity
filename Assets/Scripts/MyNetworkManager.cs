using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MyNetworkManager : NetworkManager {

	public class NetworkMessage : MessageBase {
        public char world;
    }

	public override void OnServerAddPlayer (NetworkConnection connection, short playerControllerId, NetworkReader networkReader) {
		NetworkMessage message = networkReader.ReadMessage<NetworkMessage>();
		char world = message.world;
		GameObject spawn = spawnPrefabs[world == 'A' ? 0 : 1];
		Transform startPos = GetStartPosition();
		GameObject player = (GameObject) Instantiate(spawn, startPos.position, startPos.rotation);
		player.GetComponent<Rigidbody>().isKinematic = true;
		NetworkServer.AddPlayerForConnection(connection, player, playerControllerId);
	}

	public override void OnClientConnect (NetworkConnection conn) {
		
    }

	public override void OnClientSceneChanged (NetworkConnection conn) {
		ClientScene.Ready(conn);
		NetworkMessage test = new NetworkMessage();
		test.world = GetComponent<CharacterPicker>().GetWorld();

		ClientScene.AddPlayer(conn, 0, test);
    }

	public override void OnServerRemovePlayer (NetworkConnection conn, PlayerController player) {
		if (player.gameObject != null)
			// NetworkServer.Destroy(player.gameObject);
			Debug.Log(player.gameObject.name);
	}
}
