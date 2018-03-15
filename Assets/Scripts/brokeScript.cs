using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class brokeScript : MonoBehaviour {
	public Mesh brokeMesh;
	public GameObject broke;
	public GameObject intact;
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
	}
}
