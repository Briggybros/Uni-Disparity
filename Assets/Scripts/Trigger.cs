using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Trigger : NetworkBehaviour {

	public bool requiresInteract;
    public GameObject[] targets;

    public AudioClip soundEffect;
    protected AudioSource audioout;

    // Use this for initialization
    protected virtual void Start(){
        audioout = GameObject.Find("FXSource").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    protected virtual void Update(){
    }
}
