using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreboardUI : MonoBehaviour {

	public GameObject ScoreDisplayPrefab;

	void Start () {
		StartCoroutine(Scoreboard.GetScores((results) => {
			foreach (Scoreboard.Score score in results) {
				DisplayScore(score);
			}
		}, SceneManager.GetActiveScene().name));
	}

	private void DisplayScore(Scoreboard.Score score) {
		GameObject instance = Instantiate(ScoreDisplayPrefab, new Vector3(0, 0, 0), Quaternion.identity);
		instance.transform.parent = gameObject.transform;
		Text[] texts = instance.GetComponentsInChildren<Text>();
		texts[0].text = score.rank.ToString();
		texts[1].text = score.name;
		texts[2].text = score.time.ToString();
	}
}
