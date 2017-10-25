using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePadBehaviourScript : MonoBehaviour {
    GameObject door;
    public string target;
    bool Active;
    // Use this for initialization
    void Start () {
        Active = false;
        door = GameObject.Find(target/*door's name*/);
    }
	
	// Update is called once per frame
	void Update () {
        if (Active){
            TriggerDoor();
            Active = false;
        }
		
	}

    void TriggerDoor()
    {
        door.GetComponent<DoorBehaviourScript>().SetOpenable(true);
    }

    public void ActivatePad(bool set)
    {
        Active = set;
    }
}
