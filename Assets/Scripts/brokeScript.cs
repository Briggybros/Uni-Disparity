using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokeScript : MonoBehaviour {
	public Mesh brokeMesh;
	public GameObject broke;
	public GameObject intact;
	public GameObject[] tiles;

	void SwitchReceived() {
		this.gameObject.GetComponent<Mesh>().Equals(brokeMesh);
		intact.SetActive(false);
		broke.SetActive(true);
		foreach (GameObject tile in tiles) {
			tile.SetActive(false);
		}
	}
}
