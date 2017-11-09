using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverBehaviour : MonoBehaviour {
    GameObject door;
    bool active;

	// Use this for initialization
	void Start () {
        active = false;
        door = GameObject.Find("Door"/*door's name*/);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void ActivateDoor()
    {
        door.GetComponent<DoorBehaviour>().SetOpenable(true);
    }

    public void ActivateLever(bool set)
    {
        active = set;
    }
}
