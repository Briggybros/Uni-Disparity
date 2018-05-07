using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MyNetworkManager : NetworkManager {

	public class NetworkMessage : MessageBase {
        public CharacterPicker.WORLDS world;
    }

	public override void OnServerAddPlayer (NetworkConnection connection, short playerControllerId, NetworkReader networkReader) {
		NetworkMessage message = networkReader.ReadMessage<NetworkMessage>();
		CharacterPicker.WORLDS world = message.world;
		GameObject spawn = spawnPrefabs[world == CharacterPicker.WORLDS.CAT ? 0 : CharacterPicker.IsSpectator(world) ? 2 : 1];
		Transform startPos;
		if (CharacterPicker.IsSpectator()){
			startPos = GetStartPosition();
			startPos.position += new Vector3(0, 10, 0);
		} else {
			startPos = startPositions.Find(x => x.gameObject.name == "StartPos" + world.ToString());
		}
		GameObject player = (GameObject) Instantiate(spawn, startPos.position, startPos.rotation);
		if (player.GetComponent<Rigidbody>() != null) {
			player.GetComponent<Rigidbody>().isKinematic = true;
		}
		NetworkServer.AddPlayerForConnection(connection, player, playerControllerId);
	}

	public override void OnClientConnect (NetworkConnection conn) {
		
    }

	public override void OnClientSceneChanged (NetworkConnection conn) {
		ClientScene.Ready(conn);
		NetworkMessage test = new NetworkMessage();
		test.world = CharacterPicker.GetWorld();

		ClientScene.AddPlayer(conn, 0, test);
    }

	public override void OnServerRemovePlayer (NetworkConnection conn, PlayerController player) {
		if (player.gameObject != null)
			NetworkServer.Destroy(player.gameObject);
	}
}
