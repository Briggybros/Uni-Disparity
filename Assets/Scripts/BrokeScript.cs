using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokeScript : MonoBehaviour {
	public GameObject broke;
	public GameObject intact;
	public GameObject[] tiles;

	void SwitchReceived() {
		//broke.SetActive(true);
		intact.SetActive(false);
		foreach (GameObject tile in tiles) {
			tile.SetActive(false);
		}
	}
}
