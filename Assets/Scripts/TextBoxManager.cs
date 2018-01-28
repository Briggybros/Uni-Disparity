using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextBoxManager : MonoBehaviour {

	public GameObject textBox;

	public Text theText;

	public TextAsset textFile;
	public string[] textLines;

	public int currentLine;
	public int endAtLine;

	public bool activeAtStart;

	public bool stopPlayerMovement;

	public Character character;

	private bool isTyping = false;
	private bool cancelTyping = false;

	public float typeSpeed;

	void Start () {

		character = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<Character>();

		if (textFile != null) {
			textLines = (textFile.text.Split ('\n'));
		}

		if (endAtLine == 0) {
			endAtLine = textLines.Length - 1;	
		}

		if (activeAtStart) {
			EnableTextBox();
		} 
		else {
			DisableTextBox();
		}  
	}

	void Update () {

		if (!activeAtStart) {
			return;
		} 

		if (Input.GetKeyDown(KeyCode.Return)) {

			if (!isTyping) {
				currentLine += 1;
				if (currentLine > endAtLine) {
					DisableTextBox();
				} 
				else {
					StartCoroutine(TextScroll(textLines[currentLine]));
				}
			} 
			else if (isTyping && !cancelTyping) {
				cancelTyping = true;
			}
		}
	}

	public void EnableTextBox() {
		textBox.SetActive (true);
		activeAtStart = true;
		StartCoroutine(TextScroll(textLines[currentLine]));
		if (stopPlayerMovement) {
			character.canMove = false;
		} 
	}

	public void DisableTextBox() {
		textBox.SetActive(false);
		activeAtStart = false;
		character.canMove = true;
	}

	public void ReloadScript(TextAsset theText) {
		if (theText != null) {
			textLines = new string[1];
			textLines = (theText.text.Split ('\n'));
		}
	}

	private IEnumerator TextScroll (string lineOfText) {
		int letter = 0;
		theText.text = "";
		isTyping = true;
		cancelTyping = false;
		while (isTyping && !cancelTyping && (letter < lineOfText.Length - 1)) {
			theText.text += lineOfText [letter];
			letter += 1;
			yield return new WaitForSeconds (typeSpeed);
		}
		theText.text = lineOfText;
		isTyping = false;
		cancelTyping = false;
	}
}