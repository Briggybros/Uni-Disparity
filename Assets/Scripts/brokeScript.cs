using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class brokeScript : MonoBehaviour {
	public Mesh brokeMesh;
	public GameObject broke;
	public GameObject intact;
	public GameObject[] tiles;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void SwitchReceived() {
		this.gameObject.GetComponent<Mesh>().Equals(brokeMesh);
		intact.SetActive(false);
		broke.SetActive(true);
		foreach (GameObject tile in tiles) {
			tile.SetActive(false);
		}
	}
}
