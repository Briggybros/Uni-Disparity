using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXOnly : MonoBehaviour {

	public bool enable;

	void Start () {
		if (enable && !CharacterPicker.IsSpectator()) {
			GetComponent<AudioSource>().mute = true;
		}
	}
}
