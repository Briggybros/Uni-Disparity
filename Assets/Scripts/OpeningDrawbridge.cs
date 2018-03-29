using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpeningDrawbridge : Receiver {

	public GameObject Drawbridge;
	private Quaternion newRotation;
	private bool opened;
	// Use this for initialization
	void Start () {
		opened = false;
	}
	
	// Update is called once per frame
	protected override void SwitchReceived () {
		Debug.Log("switch received");
		StopAllCoroutines();
		if (opened == false) {
			opened = true;
			StartCoroutine(OpenDrawbridge());
		}
		else {
			opened = false;
			StartCoroutine(CloseDrawbridge());
		}
	}
	
	IEnumerator OpenDrawbridge () {
		Debug.Log("setting new rotation");
		newRotation = Quaternion.Euler(-90, Drawbridge.transform.rotation.y, Drawbridge.transform.rotation.z);
		Debug.Log("maybe doing the rotation");
		while (Drawbridge.transform.rotation.x > -90) {
			Drawbridge.transform.rotation = Quaternion.Lerp(Drawbridge.transform.rotation, newRotation, 2*Time.deltaTime);
			yield return 0;
		}
	}

	IEnumerator CloseDrawbridge () {
		newRotation = Quaternion.Euler(0, Drawbridge.transform.rotation.y, Drawbridge.transform.rotation.z);
		while (Drawbridge.transform.rotation.x < 0) {
			Drawbridge.transform.rotation = Quaternion.Lerp(Drawbridge.transform.rotation, newRotation, 2*Time.deltaTime);
			yield return 0;
		}
	}
}
