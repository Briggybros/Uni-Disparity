using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

// selfie stick since we essentially stick a camera on the end of a stick move it around
public class SelfieStickController : MonoBehaviour {

	private bool automaticControl = false;
	private SpawnOnTargetDetect spawnOnTargetDetect;

	// Use this for initialization
	void Start () {
		transform.position = Vector3.zero;
		GameObject.FindWithTag("MainCamera").SetActive(false);
		spawnOnTargetDetect = GameObject.FindGameObjectWithTag("ImageTarget").GetComponent<SpawnOnTargetDetect>();

		spawnOnTargetDetect.OnTrackableStateChanged(TrackableBehaviour.Status.NOT_FOUND, TrackableBehaviour.Status.DETECTED);

        GameObject[] canvasComponents = GameObject.FindGameObjectsWithTag("UI");
		foreach (var component in canvasComponents) {
			Debug.Log(component.name);
			if (component.name == "UI Canvas" && CharacterPicker.IsSpectator()) {
            	component.GetComponent<Canvas>().enabled = false;
			}
		}


	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.P)) {
			CharacterPicker.ChangeSpectatorFocus();
			spawnOnTargetDetect.OnTrackableStateChanged(TrackableBehaviour.Status.NOT_FOUND, TrackableBehaviour.Status.DETECTED);
		}
		if (automaticControl) {
			transform.RotateAround(Vector3.zero, Vector3.up, 10 * Time.deltaTime);
			if(Input.GetKey(KeyCode.Space)) {
				automaticControl = !automaticControl;
			}
		}
		else {
			if (Input.GetKey(KeyCode.D)) {
				transform.RotateAround(Vector3.zero, Vector3.up, 20 * Time.deltaTime);
			}
			if (Input.GetKey(KeyCode.W)) {
				transform.Rotate(20 * Vector3.forward * Time.deltaTime);
			}
			if (Input.GetKey(KeyCode.A)) {
				transform.RotateAround(Vector3.zero, Vector3.up, -20 * Time.deltaTime);
			}
			if (Input.GetKey(KeyCode.S)) {
				transform.Rotate(20 * Vector3.back * Time.deltaTime);
			}
			if(Input.GetKey(KeyCode.Space)) {
					automaticControl = !automaticControl;
			}
		}
	}
}
