using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PauseUI : MonoBehaviour {
	public void Disconnect() {
		NetworkManager.singleton.StopHost();
		NetworkManager.singleton.onlineScene = null;
		Time.timeScale = 1;
	}
}
