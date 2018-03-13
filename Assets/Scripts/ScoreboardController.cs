using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreboardController : MonoBehaviour {

	public Text TimeText;
	public GameObject NamePanel;
	public GameObject ScorePanel;
	public bool isTimeStarted = false;
	private float timeElapsed = 0;

	void Start () {
		
	}
	
	void Update () {
		if (isTimeStarted) {
			timeElapsed += Time.deltaTime;
			TimeSpan timeSpan = TimeSpan.FromSeconds(timeElapsed);
			TimeText.text = string.Format("{0:00}:{1:00}", timeSpan.Minutes, timeSpan.Seconds);
		}
	}

	public void StartTimer() {
		isTimeStarted = true;
	}

	public void EndGame() {
		isTimeStarted = false;
		NamePanel.SetActive(true);
	}

	public void OnNameEntered() {
		string name = NamePanel.GetComponentInChildren<InputField>().text;
		Scoreboard.Score score = new Scoreboard.Score(SceneManager.GetActiveScene().name, name, timeElapsed);
		ScorePanel.GetComponent<ScoreboardUI>().ShowScores();
		StartCoroutine(Scoreboard.PostScore((results) => {
			ScorePanel.GetComponent<ScoreboardUI>().ShowScores(new Scoreboard.Score[] {results});
		}, score));
	}
}
