using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dissappear : MonoBehaviour {

	private int count;
	// Use this for initialization
	void Start () {
		count = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (this.tag == "Bopped") {
			count++;
			this.transform.Translate(Vector3.up * Time.deltaTime);
		}
		if (count > 100) {
			this.gameObject.GetComponent<Renderer>().enabled = false;
		}
	}
}
