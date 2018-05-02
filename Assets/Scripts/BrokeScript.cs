using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BrokeScript : NetworkBehaviour {
	public GameObject broke;
	public GameObject intact;
	public GameObject[] tiles;

	void SwitchReceived() {
		//broke.SetActive(true);
		intact.SetActive(false);
		if (isServer) {
			foreach (GameObject tile in tiles) {
				tile.SetActive(false);
			}
		}
	}
}
