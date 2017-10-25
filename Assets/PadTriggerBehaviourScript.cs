using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PadTriggerBehaviourScript : MonoBehaviour {
    GameObject pad;
    public string target;
    public string owner;

    // Use this for initialization
    void Start () {
        pad = GameObject.Find(target/*pad's name*/);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == owner/*ensure it the the player name, or the object name*/)
        {
            pad.GetComponent<PressurePadBehaviourScript>().ActivatePad(true);
        }
    }
}
