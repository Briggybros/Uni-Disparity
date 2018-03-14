using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// selfie stick since we essentially stick a camera on the end of a stick move it around
public class SelfieStickController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		transform.position = Vector3.zero;
		GameObject.FindWithTag("MainCamera").SetActive(false);
		
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
		if (Input.GetKey(KeyCode.D)) {
            transform.Rotate(20 * Vector3.right * Time.deltaTime);
        }
		if (Input.GetKey(KeyCode.W)) {
			transform.Rotate(20 * Vector3.forward * Time.deltaTime);
		}
		if (Input.GetKey(KeyCode.A)) {
			transform.Rotate(20 * Vector3.left * Time.deltaTime);
		}
		if (Input.GetKey(KeyCode.S)) {
			transform.Rotate(20 * Vector3.back * Time.deltaTime);
		}
	}
}
