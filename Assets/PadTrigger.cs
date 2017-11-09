using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PadTrigger : MonoBehaviour {
    GameObject pad;

	// Use this for initialization
	void Start () {
        pad = GameObject.Find("Pad"/*pad's name*/);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider myTrigger)
    {
        if(myTrigger.GameObject.name == "somethign"/*ensure it the the player name, or the object name*/)
        {
            pad.GetComponent<PressureBehaviour>().ActivatePad(true);
        }
    }
}
