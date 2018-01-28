using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Vuforia;

public class SpawnOnTargetDetect : MonoBehaviour, ITrackableEventHandler {

	private TrackableBehaviour trackableBehaviour;
	private bool isServerAdded = false;
	private bool isTracked = false;
	private bool isSpawned = false;
	private NetworkConnection conn;
	private short playerControllerId;
	// private GameObject[] spawnpoints;

    void Start () {
		// spawnpoints = (GameObject[]) FindObjectsOfType(typeof(NetworkStartPosition));
		trackableBehaviour = GetComponent<TrackableBehaviour>();
		if (trackableBehaviour) {
			trackableBehaviour.RegisterTrackableEventHandler(this);
		}
	}

	public void OnServerAddPlayer (NetworkConnection conn, short playerControllerId) {
		isServerAdded = true;
		this.conn = conn;
		this.playerControllerId = playerControllerId;
		if (isServerAdded && isTracked && !isSpawned) {
			AddPlayer();
		}
	}

    public void OnTrackableStateChanged(TrackableBehaviour.Status previousStatus, TrackableBehaviour.Status newStatus)
    {
        if (newStatus == TrackableBehaviour.Status.DETECTED ||
			newStatus == TrackableBehaviour.Status.TRACKED ||
			newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED) {
				isTracked = true;
				if (isServerAdded && isTracked && !isSpawned) {
					AddPlayer();
				}
			}
    }

	private void AddPlayer () {
		GameObject playerPrefab = NetworkManager.singleton.playerPrefab;
		Transform startPos = NetworkManager.singleton.GetStartPosition();
		GameObject player = (GameObject) Instantiate(playerPrefab, startPos.position, Quaternion.identity);
		NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
		isSpawned = true;
	}
}