using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListenerScript : MonoBehaviour {


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //Add future trigger events to here, such as interaction
    void EnterFlag(){
        base.gameObject.BroadcastMessage("ColliderEnter");
    }

    void ExitFlag(){
        base.gameObject.BroadcastMessage("ColliderExit");
    }

    void WithinFlag(){
        base.gameObject.BroadcastMessage("ColliderWithin");
    }
}
