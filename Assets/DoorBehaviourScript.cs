using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBehaviour : MonoBehaviour {
    bool open;
	// Use this for initialization
	void Start () {
        open = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (open)
        {
            //Open the door
        }
        else
        {
            //do whatever doors do while they wait
        }
	}

    public void SetOpenable(bool set)
    {
        open = set;
    }
}
