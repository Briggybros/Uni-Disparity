using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreboardUI : MonoBehaviour
{
  public GameObject ScoreDisplayPrefab;

  public void ShowScores(Scoreboard.Score[] scores = null)
  {
    Scoreboard.ScoresCallback callback = (results) =>
    {
      foreach (Scoreboard.Score score in results)
      {
        DisplayScore(score);
      }
    };
    if (scores == null)
    {
      StartCoroutine(Scoreboard.GetScores(callback, SceneManager.GetActiveScene().name));
    }
    else
    {
      callback(scores);
    }
  }

  private void DisplayScore(Scoreboard.Score score)
  {
    GameObject instance = Instantiate(ScoreDisplayPrefab, new Vector3(0, 0, 0), Quaternion.identity);
    instance.transform.parent = gameObject.transform;
    Text[] texts = instance.GetComponentsInChildren<Text>();
    texts[0].text = score.rank.ToString();
    texts[1].text = score.name;
    TimeSpan timeSpan = TimeSpan.FromSeconds(score.time);
    texts[2].text = string.Format("{0:00}:{1:00}:{2:00}", timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds); ;
  }
}
