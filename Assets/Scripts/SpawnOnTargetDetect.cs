using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Vuforia;

[RequireComponent(typeof(CharacterPicker))]
public class SpawnOnTargetDetect : MonoBehaviour, ITrackableEventHandler {

	public ScoreboardController scoreboard;

	private TrackableBehaviour trackableBehaviour;
	private CharacterPicker.WORLDS otherWorld;
	private bool isTracking, isActive;

    void Start () {
		trackableBehaviour = GetComponent<TrackableBehaviour>();
		if (trackableBehaviour) {
			trackableBehaviour.RegisterTrackableEventHandler(this);
		}
	}

	public void Update () {
		if (!CharacterPicker.IsSpectator()) {
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
		Debug.Log("hello");
		otherWorld = CharacterPicker.GetOtherWorld();
		if (scoreboard != null) {
			if (!scoreboard.isTimeStarted) {
				scoreboard.StartTimer();
			}
		} else {
			Debug.LogWarning("There's no scoreboard provided, are you sure this was intended?");
		}
		isTracking = true;
        Renderer[] rendererComponents = GetComponentsInChildren<Renderer>(true);
        Collider[] colliderComponents = GetComponentsInChildren<Collider>(true);
        Canvas[] canvasComponents = GetComponentsInChildren<Canvas>(true);

		// render components ONLY in the players world
        foreach (var component in rendererComponents) {
			if (component.GetComponent<Disparity>() != null && component.GetComponent<Disparity>().World == otherWorld) {
            	component.enabled = false;
			} else {
				component.enabled = true;
			}
		}

		//render colliders only in players world
        foreach (var component in colliderComponents) {
			if (component.gameObject.GetComponent<Disparity>() != null &&
				component.gameObject.GetComponent<Disparity>().World == otherWorld &&
				!component.gameObject.GetComponent<Disparity>().isColliderShared) {
            	component.enabled = false;
			} else {
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