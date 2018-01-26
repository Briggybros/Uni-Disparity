using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using UnityEngine.UI;

[RequireComponent(typeof(NetworkManager))]
public class NetworkUI : MonoBehaviour {

	public GameObject networkingUI;
	private NetworkManager networkManager;
	private GameObject container;

	// Use this for initialization
	void Start () {
		networkManager = GetComponent<NetworkManager>();

        container = new GameObject("UI");
        Canvas canvas = container.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;

        CanvasScaler canvasScaler = container.AddComponent<CanvasScaler>();
        canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        canvasScaler.referenceResolution = new Vector2(1080, 720);
        canvasScaler.matchWidthOrHeight = 0.5f;

        container.AddComponent<GraphicRaycaster>();
        container.layer = 5;

        GameObject instantiated = Instantiate(networkingUI, new Vector2(0, 0), Quaternion.identity);
        instantiated.transform.SetParent(container.transform, false);

		instantiated.GetComponent<Button>().onClick.AddListener(Clicked);
	}

	private void Clicked () {
		networkManager.StartMatchMaker();

		GameObject instantiated1 = Instantiate(networkingUI, new Vector2(0, 0), Quaternion.identity);
		instantiated1.GetComponentInChildren<Text>().text = "Create Internet Match";
        instantiated1.transform.SetParent(container.transform, false);
		instantiated1.GetComponent<Button>().onClick.AddListener(() => CreateInternetMatch("default"));

		GameObject instantiated2 = Instantiate(networkingUI, new Vector2(0, 0), Quaternion.identity);
		instantiated1.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 30);
		instantiated2.GetComponentInChildren<Text>().text = "Find Internet Match";
        instantiated2.transform.SetParent(container.transform, false);
		instantiated2.GetComponent<Button>().onClick.AddListener(() => FindInternetMatch("default"));
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
