using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePadBehaviour : MonoBehaviour {
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

    void TriggerDoor()
    {
        door.GetComponent<DoorBehaviour>().SetOpenable(true);
    }

    public void ActivatePad(bool set)
    {
        active = set;
    }
}
