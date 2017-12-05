using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour {

	// Use this for initialization
	void Start() {

	}

	// Update is called once per frame
	void Update() {

	}

	/*protected virtual void OnTriggerStay(Collider other) {
		if (other.GetComponent<Character>().isInteracting()) {
			base.gameObject.BroadcastMessage("Pressed");
			other.GetComponent<Character>().setInteract();
			Debug.Log("bllloooop");
		}
	}*/
}
