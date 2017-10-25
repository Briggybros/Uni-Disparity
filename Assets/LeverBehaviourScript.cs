using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverBehaviourScript : MonoBehaviour {
    GameObject door;
    public string target;
    bool active;

	// Use this for initialization
	void Start () {
        active = false;
        door = GameObject.Find(target/*door's name*/);
	}
	
	// Update is called once per frame
	void Update () {
        if (active)
        {
            ActivateDoor();
            active = false;
        }
    }

    void ActivateDoor()
    {
        door.GetComponent<DoorBehaviourScript>().SetOpenable(true);
    }

    public void ActivateLever(bool set)
    {
        active = set;
    }
}
