using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Vuforia;

[RequireComponent(typeof(NetworkIdentity))]
public class SelfieStickController : NetworkBehaviour {

	private float timeSinceLastInteract = 0.0f;
	private const float AUTO_TIMEOUT = 0.5f;
	private GameObject CATDOGCamera;

	void Start () {
		if (!isLocalPlayer) {
			return;
		}
		transform.position = Vector3.zero;
		GameObject.FindWithTag("MainCamera").SetActive(false);
		foreach (Transform childTransform in transform) {
			GameObject child = childTransform.gameObject;
			if (child.GetComponent<Camera>() != null) {
				child.SetActive(true);
			}
		}

		CATDOGCamera = GameObject.Find("SpectatorCameraDOG");
        GameObject[] canvasComponents = GameObject.FindGameObjectsWithTag("UI");
		foreach (var component in canvasComponents) {
			if (component.name == "UI Canvas" && CharacterPicker.IsSpectator()) {
            	component.GetComponent<Canvas>().enabled = false;
			}
		}
	}
	
	void Update () {
		if (!isLocalPlayer) {
			return;
		}
		if (timeSinceLastInteract >= AUTO_TIMEOUT) {
			transform.RotateAround(Vector3.zero, Vector3.up, 10 * Time.deltaTime);
		}
		timeSinceLastInteract += Time.deltaTime;
		float angle = Vector3.Angle(CATDOGCamera.transform.position, Vector3.up);
		if (Input.GetKey(KeyCode.D)) {
			transform.RotateAround(Vector3.zero, Vector3.up, 20 * Time.deltaTime);
			timeSinceLastInteract = 0;
		}
		if (Input.GetKey(KeyCode.W)) {
			if (!(angle < 5.0f)) {
				transform.Rotate(20 * Vector3.forward * Time.deltaTime);
			}
			timeSinceLastInteract = 0;
		}
		if (Input.GetKey(KeyCode.A)) {
			transform.RotateAround(Vector3.zero, Vector3.up, -20 * Time.deltaTime);
			timeSinceLastInteract = 0;
		}
		if (Input.GetKey(KeyCode.S)) {
			if (!(angle > 80.0f)){
				transform.Rotate(20 * Vector3.back * Time.deltaTime);
			}
			timeSinceLastInteract = 0;
		}
	}
}
