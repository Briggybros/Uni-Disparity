using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class ScoreboardController : NetworkBehaviour {

	public Text TimeText;
	public GameObject NamePanel;
	public GameObject ScorePanel;
	public bool isTimeStarted = false;
	private float timeElapsed = 0;
	
	void Update () {
		if (isTimeStarted) {
			timeElapsed += Time.deltaTime;
			TimeSpan timeSpan = TimeSpan.FromSeconds(timeElapsed);
			TimeText.text = string.Format("{0:00}:{1:00}", timeSpan.Minutes, timeSpan.Seconds);
		}
	}


	[Command]
	public void CmdStartTimer() {
		if (NetworkServer.active) {
			RpcStartTimer();
		}
	}

	[ClientRpc]
	private void RpcStartTimer() {
		isTimeStarted = true;
	}

	[ClientRpc]
	private void RpcStopTimer(float time) {
		isTimeStarted = false;
		timeElapsed = time;
	}

	public void EndGame() {
		if (NetworkServer.active) {
			RpcStopTimer(timeElapsed);
			NamePanel.SetActive(true);
		} else {
			ScorePanel.SetActive(true);
			ScorePanel.GetComponent<ScoreboardUI>().ShowScores();
		}
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
