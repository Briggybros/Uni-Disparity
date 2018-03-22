using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class MatchJoinPanelInit : MonoBehaviour {

	public GameObject joinButton;
	public GameObject spectateButton;
	public GameObject levelText;

	public void Init(string levelName, UnityAction onJoinMatchFunc, UnityAction onSpectateMatchFunc) {
		levelText.GetComponent<Text>().text = levelName;
		joinButton.GetComponent<Button>().onClick.AddListener(onJoinMatchFunc);
		spectateButton.GetComponent<Button>().onClick.AddListener(onSpectateMatchFunc);
	}
}
