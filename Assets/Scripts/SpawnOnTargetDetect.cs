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
	private char otherWorld;
	// private GameObject[] spawnpoints;

    void Start () {
		// spawnpoints = (GameObject[]) FindObjectsOfType(typeof(NetworkStartPosition));
		trackableBehaviour = GetComponent<TrackableBehaviour>();
		if (trackableBehaviour) {
			trackableBehaviour.RegisterTrackableEventHandler(this);
		}
		otherWorld = Network.isServer ? 'B' : 'A';
		Debug.Log(otherWorld);
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
			OnTrackingFound();
			isTracked = true;
			if (isServerAdded && isTracked && !isSpawned) {
				AddPlayer();
			}
		} else if (previousStatus == TrackableBehaviour.Status.TRACKED && newStatus == TrackableBehaviour.Status.NOT_FOUND) {
			OnTrackingLost();
		} else {
			OnTrackingLost();
		}
    }

	protected virtual void OnTrackingFound()
    {
        var rendererComponents = GetComponentsInChildren<Renderer>(true);
        var colliderComponents = GetComponentsInChildren<Collider>(true);
        var canvasComponents = GetComponentsInChildren<Canvas>(true);

        foreach (var component in rendererComponents) {
			Debug.Log(component.gameObject.name);
			if (component.gameObject.name[component.gameObject.name.Length-1] != otherWorld) {
            	component.enabled = true;
			}
		}

        foreach (var component in colliderComponents)
            component.enabled = true;

        foreach (var component in canvasComponents)
            component.enabled = true;
    }


    protected virtual void OnTrackingLost()
    {
        var rendererComponents = GetComponentsInChildren<Renderer>(true);
        var colliderComponents = GetComponentsInChildren<Collider>(true);
        var canvasComponents = GetComponentsInChildren<Canvas>(true);

        foreach (var component in rendererComponents)
            component.enabled = false;

        foreach (var component in colliderComponents)
            component.enabled = false;

        foreach (var component in canvasComponents)
            component.enabled = false;
    }

	private void AddPlayer () {
		GameObject playerPrefab = NetworkManager.singleton.spawnPrefabs[otherWorld == 'B' ? 0 : 1];
		Transform startPos = NetworkManager.singleton.GetStartPosition();
		GameObject player = (GameObject) Instantiate(playerPrefab, startPos.position, Quaternion.identity);
		NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
		isSpawned = true;
	}
}