using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using UnityEngine.UI;

[RequireComponent(typeof(NetworkManager))]
public class NetworkUI : MonoBehaviour {

	public GameObject buttonPrefab;
	public string[] levels;

	private NetworkManager networkManager;
	private GameObject uiContainer;

	private GameObject MakeButton (GameObject container, string text, Vector2 position, UnityAction clickListener = null) {
		GameObject button = Instantiate(buttonPrefab, new Vector2(0, 0), Quaternion.identity);
		button.GetComponentInChildren<Text>().text = text;
		button.GetComponent<RectTransform>().anchoredPosition = position;
		if (clickListener != null) {
			button.GetComponent<Button>().onClick.AddListener(clickListener);
		}
		button.transform.SetParent(container.transform, false);

		return button;
	}

	// Use this for initialization
	void Start () {
		networkManager = NetworkManager.singleton;

        uiContainer = new GameObject("UI");
        Canvas canvas = uiContainer.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;

        CanvasScaler canvasScaler = uiContainer.AddComponent<CanvasScaler>();
        canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        canvasScaler.referenceResolution = new Vector2(1080, 720);
        canvasScaler.matchWidthOrHeight = 0.5f;

        uiContainer.AddComponent<GraphicRaycaster>();
        uiContainer.layer = 5;

        GameObject startButton = MakeButton(uiContainer, "Start", new Vector2(0, 0));
		startButton.GetComponent<Button>().onClick.AddListener(() => {
			Destroy(startButton);
			Clicked();
		});
	}

	private void Clicked () {
		networkManager.StartMatchMaker();

		GameObject createButton = MakeButton(uiContainer, "Create Internet Match", new Vector2(0, 40));

		GameObject findButton = MakeButton(uiContainer, "Find Internet Match", new Vector2(0, -40));

		createButton.GetComponent<Button>().onClick.AddListener(() => {
			LevelSelect();
			Destroy(createButton);
			Destroy(findButton);
			// Make function to clear up buttons instead
		});

		findButton.GetComponent<Button>().onClick.AddListener(() => {
			FindInternetMatch("default");
			Destroy(createButton);
			Destroy(findButton);
		});
	}

	private void LevelSelect () {
		foreach (string level in levels) {
			MakeButton(uiContainer, level, new Vector2(0, 0), () => {
				networkManager.onlineScene = level;
				CreateInternetMatch("default");
			});
		}
		// Need to clear up buttons
	}

	private void CreateInternetMatch (string matchName) {
		networkManager.matchMaker.CreateMatch(matchName, 2, true, "", "", "", 0, 0, OnInternetMatchCreate);
	}

	private void OnInternetMatchCreate (bool success, string extendedInfo, MatchInfo matchInfo) {
		if (success) {
			NetworkServer.Listen(matchInfo, 9000);

			networkManager.StartHost(matchInfo);
		}
		// Needs error
	}

	private void FindInternetMatch (string matchName) {
		networkManager.matchMaker.ListMatches(0 ,10, matchName, true, 0, 0, OnInternetMatchList);
	}

	private void OnInternetMatchList(bool success, string extendedInfo, List<MatchInfoSnapshot> internetMatches) {
		if (success) {
			if (internetMatches.Count != 0) {
				networkManager.matchMaker.JoinMatch(internetMatches[0].networkId, "", "", "", 0, 0, OnJoinInternetMatch);
			}
			// Needs no matches
		}
		// Needs error
	}

	private void OnJoinInternetMatch(bool success, string extendedInfo, MatchInfo matchInfo) {
		if (success) {
			networkManager.StartClient(matchInfo);
		}
		// Needs error
	}
}
