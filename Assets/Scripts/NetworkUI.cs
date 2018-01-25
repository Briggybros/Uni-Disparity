using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
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
        canvasScaler.matchWidthOrHeight = 0;

        container.AddComponent<GraphicRaycaster>();
        container.layer = 5;

        GameObject instantiated = Instantiate(networkingUI, new Vector2(0, 0), Quaternion.identity);
        instantiated.transform.SetParent(container.transform, false);

		instantiated.GetComponent<Button>().onClick.AddListener(Clicked);
	}

	void Clicked() {
		networkManager.StartMatchMaker();

		GameObject instantiated1 = Instantiate(networkingUI, new Vector2(0, 0), Quaternion.identity);
		instantiated1.GetComponentInChildren<Text>().text = "Create Internet Match";
        instantiated1.transform.SetParent(container.transform, false);

		GameObject instantiated2 = Instantiate(networkingUI, new Vector2(0, 0), Quaternion.identity);
		instantiated2.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 30);
		instantiated2.GetComponentInChildren<Text>().text = "Find Internet Match";
        instantiated2.transform.SetParent(container.transform, false);
	}
}
