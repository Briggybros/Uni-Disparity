using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Vuforia;

[RequireComponent(typeof(CharacterPicker))]
public class SpawnOnTargetDetect : MonoBehaviour, ITrackableEventHandler {

	private TrackableBehaviour trackableBehaviour;
	private char otherWorld;
	private bool isTracking, isActive;

    void Start () {
		trackableBehaviour = GetComponent<TrackableBehaviour>();
		if (trackableBehaviour) {
			trackableBehaviour.RegisterTrackableEventHandler(this);
		}
		otherWorld = CharacterPicker.GetOtherWorld();
	}

	public void Update () {
		if (CharacterPicker.IsSpectator()) {
			if (Input.GetKey(KeyCode.P)){
				Debug.Log(otherWorld);
				CharacterPicker.ChangeSpectatorFocus();
				otherWorld = CharacterPicker.GetOtherWorld();
				Debug.Log(otherWorld);
				OnTrackingFound();
			}
			GameObject[] cameras = GameObject.FindGameObjectsWithTag("MainCamera");
			foreach (var camera in cameras) {
				if (camera.name.Contains("SpectatorCamera")) {
					foreach (var other in cameras) {
						if (other != camera) {
							other.GetComponent<Camera>().enabled = false;
							other.GetComponent<VuforiaBehaviour>().enabled = false;
							camera.GetComponent<Camera>().enabled = true;
						}
					}
				} 
			}
		} else {
			var players = GameObject.FindGameObjectsWithTag("Player");
			if (isTracking && !isActive) {	
				foreach (var player in players) {
					player.gameObject.GetComponent<Rigidbody>().isKinematic = false;
					isActive = true;
				}
			} else if (!isTracking && isActive) {
				foreach (var player in players) {
					player.gameObject.GetComponent<Rigidbody>().isKinematic = true;
					isActive = false;
				}
			}
		}
	}

    public void OnTrackableStateChanged(TrackableBehaviour.Status previousStatus, TrackableBehaviour.Status newStatus)
    {
        if (newStatus == TrackableBehaviour.Status.DETECTED ||
			newStatus == TrackableBehaviour.Status.TRACKED ||
			newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED) {
			OnTrackingFound();
		} else if (previousStatus == TrackableBehaviour.Status.TRACKED && newStatus == TrackableBehaviour.Status.NOT_FOUND) {
			OnTrackingLost();
		} else {
			OnTrackingLost();
		}
    }

	protected virtual void OnTrackingFound()
    {
		isTracking = true;
        var rendererComponents = GetComponentsInChildren<Renderer>(true);
        var colliderComponents = GetComponentsInChildren<Collider>(true);
        var canvasComponents = GetComponentsInChildren<Canvas>(true);

		// render components ONLY in the players world
        foreach (var component in rendererComponents) {
			int strLen = component.gameObject.name.Length;
			string name = component.gameObject.name;
			if (component.gameObject.GetComponent<disparity>.World != otherWorld) {
            	component.enabled = true;
			}
		}

		//render colliders only in players world
        foreach (var component in colliderComponents) {
			int strLen = component.gameObject.name.Length;
			string name = component.gameObject.name;
			if (component.gameObject.GetComponent<disparity>.World != otherWorld ||
				component.gameObject.GetComponent<disparity>.isColliderShared) {
            	component.enabled = true;
			} else if (name[strLen-2] != otherWorld) {
				component.enabled = true;
			}
		}
		
		// remove buttons if spectator
        foreach (var component in canvasComponents) {
			component.enabled = true;
		}
    }

    protected virtual void OnTrackingLost()
    {
		isTracking = false;
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
}