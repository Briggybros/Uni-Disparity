using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Trigger : NetworkBehaviour {

	public bool requiresInteract;
    public GameObject[] targets;


    // Use this for initialization
    protected virtual void Start(){
    }

    // Update is called once per frame
    protected virtual void Update(){
    }
}
