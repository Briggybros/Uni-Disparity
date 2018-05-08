using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterPicker))]
public class NetworkUI : MonoBehaviour {


	public NetworkDiscovery discovery;
    public AudioClip buttonClick;
	public GameObject buttonPrefab;
	public GameObject matchJoinPanelPrefab;
	public GameObject levelSelectPanel;
	public GameObject matchSelectPanel;
	public GameObject errorMessageObject;
	public GameObject graphicsSlider;
	public GameObject graphicsText;
	public GameObject muteButton;
	public GameObject loadingPanel;
	public string[] levels;
	public bool isInternet = false;

	private NetworkManager networkManager;
    private AudioSource audioout;
	private IEnumerator errorHideCoroutine = null;
	private bool lookingForMatches = false;

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

	private IEnumerator HideError (float timeout) {
		yield return new WaitForSeconds(timeout);
		errorMessageObject.SetActive(false);
		errorHideCoroutine = null;
	}

	private void ShowError (string message) {
		if (errorHideCoroutine != null) {
			StopCoroutine(errorHideCoroutine);
		}
		errorMessageObject.GetComponent<Text>().text = message;
		errorMessageObject.SetActive(true);
		errorHideCoroutine = HideError(5);
		StartCoroutine(errorHideCoroutine);
	}

	void Start () {
		networkManager = NetworkManager.singleton;
		graphicsSlider.GetComponent<Slider>().maxValue = QualitySettings.names.Length - 1;
		graphicsSlider.GetComponent<Slider>().value = QualitySettings.GetQualityLevel();
        audioout = GameObject.Find("AudioOutput").GetComponent<AudioSource>();
	}

	public void StartButtonClicked (bool isInternet) {
		this.isInternet = isInternet;
		if (isInternet) {
			networkManager.StartMatchMaker();
		} else {
			discovery.Initialize();
		}
	}

	public void StopMatchMaker () {
		if (isInternet) {
			networkManager.StopMatchMaker();
		} else {
			lookingForMatches = false;
			// discovery.StopBroadcast();
		}
	}

	public void LevelSelect (Text textObject) {
		string matchName = textObject.text != "" ? textObject.text : "default";
		foreach (Transform child in levelSelectPanel.transform) {
			GameObject.Destroy(child.gameObject);
		}
		foreach (string level in levels) {
			MakeButton(levelSelectPanel, level, new Vector2(0, 0), () => {
				networkManager.onlineScene = level;
				CreateInternetMatch(matchName);
			});
		}
	}

	public void CreateInternetMatch (string matchName) {
		CharacterPicker.SetWorld(CharacterPicker.WORLDS.CAT);
		if (isInternet) {
			networkManager.matchMaker.CreateMatch(matchName, 4, true, "", "", "", 0, 0, OnInternetMatchCreate);
		} else {
			discovery.StartAsServer();
			networkManager.StartHost();
		}
        ShowLevelPanel();
	}

	private void OnInternetMatchCreate (bool success, string extendedInfo, MatchInfo matchInfo) {
		if (success) {
			NetworkServer.Listen(matchInfo, 9000);
			networkManager.StartHost(matchInfo);
		} else {
			ShowError("Failed to connect to the match");
		}
	}

    void ShowLevelPanel()
    {
        GameObject.Find("BackingOutput").GetComponent<AudioSource>().mute = true;
        audioout.PlayOneShot(buttonClick);
        loadingPanel.SetActive(true);
    }

	public void FindInternetMatch (Text textObject) {
		if (isInternet) {
			networkManager.matchMaker.ListMatches(0 ,10, "", true, 0, 0, OnInternetMatchList);
		} else {
			discovery.StartAsClient();
			lookingForMatches = true;
			StartCoroutine(FindMatches());
		}
	}

	private IEnumerator FindMatches() {
		while (lookingForMatches) {
			foreach (Transform child in matchSelectPanel.transform) {
				GameObject.Destroy(child.gameObject);
			}
			foreach (string addr in discovery.broadcastsReceived.Keys) {
				AddClientMatch(addr);
			}
			yield return new WaitForSeconds(1);
		}
	}

	public void AddClientMatch(string fromAddress) {
		GameObject panel = Instantiate(matchJoinPanelPrefab, new Vector2(0, 0), Quaternion.identity);
		panel.transform.SetParent(matchSelectPanel.transform, false);
		panel.GetComponent<MatchJoinPanelInit>().Init(fromAddress, () => {
			CharacterPicker.SetWorld(CharacterPicker.WORLDS.DOG);
			networkManager.networkAddress = fromAddress;
			networkManager.StartClient();
			this.lookingForMatches = false;
			ShowLevelPanel();
		}, () => {
			CharacterPicker.SetWorld(CharacterPicker.WORLDS.SPECTATOR);
			networkManager.networkAddress = fromAddress;
			networkManager.StartClient();
			this.lookingForMatches = false;
			ShowLevelPanel();
		});
	}

	private void OnInternetMatchList(bool success, string extendedInfo, List<MatchInfoSnapshot> internetMatches) {
		foreach (Transform child in matchSelectPanel.transform) {
			GameObject.Destroy(child.gameObject);
		}
		if (success) {
			if (internetMatches.Count != 0) {
				foreach (MatchInfoSnapshot match in internetMatches) {
					GameObject panel = Instantiate(matchJoinPanelPrefab, new Vector2(0, 0), Quaternion.identity);
					panel.transform.SetParent(matchSelectPanel.transform, false);
					panel.GetComponent<MatchJoinPanelInit>().Init(match.name, () => {
						CharacterPicker.SetWorld(CharacterPicker.WORLDS.DOG);
						networkManager.matchMaker.JoinMatch(match.networkId, "", "", "", 0, 0, OnJoinInternetMatch);
						ShowLevelPanel();
					}, () => {
						CharacterPicker.SetWorld(CharacterPicker.WORLDS.SPECTATOR);
						networkManager.matchMaker.JoinMatch(match.networkId, "", "", "", 0, 0, OnJoinInternetMatch);
						ShowLevelPanel();
					});
				}
			} else {
				ShowError("No matches found");
			}
		} else {
			ShowError("Unable to find matches");
		}
	}

	private void OnJoinInternetMatch(bool success, string extendedInfo, MatchInfo matchInfo) {
		if (success) {
			networkManager.StartClient(matchInfo);
		} else {
			ShowError("Failed to join the match");
		}
	}

	public void DisconnectInternetMatch() {
		networkManager.StopHost();
		networkManager.onlineScene = null;
	}

	public void ChangeGraphics() {
		int val = (int) graphicsSlider.GetComponent<Slider>().value;
		graphicsText.GetComponent<Text>().text = "Quality: " + QualitySettings.names[val];
		QualitySettings.SetQualityLevel(val, true);
	}

	public void MuteAudio() {
		AudioListener.pause = !AudioListener.pause;
		muteButton.GetComponentInChildren<Text>().text = AudioListener.pause ? "Unmute" : "Mute";
	}
}
