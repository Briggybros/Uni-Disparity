using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateTextAtLine : MonoBehaviour {

	public TextAsset theText;

	public int startLine;
	public int endLine;

	public TextBoxManager textManager;

	public bool destroyWhenActivated;

	public bool requireButtonPress;
	private bool waitForPress;

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {

		if (waitForPress && Input.GetKeyDown(KeyCode.E)){
			textManager.ReloadScript(theText);
			textManager.currentLine = startLine;
			textManager.endAtLine = endLine;
			textManager.EnableTextBox();

			if (destroyWhenActivated) {
				Destroy(gameObject);
			}
		}
	}

	void OnTriggerEnter(Collider other){

		if (other.name == "CatObj") {
			if (requireButtonPress) {
				waitForPress = true;
				return;
			}

			textManager.ReloadScript(theText);
			textManager.currentLine = startLine;
			textManager.endAtLine = endLine;
			textManager.EnableTextBox();

			if (destroyWhenActivated) {
				Destroy(gameObject);
			}
		}
	}

	void OnTriggerExit(Collider other){

		if (other.name == "CatObj") {
			textManager.DisableTextBox();
			waitForPress = false;
		}
	}
}