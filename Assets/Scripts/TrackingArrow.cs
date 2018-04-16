using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TrackingArrow : NetworkBehaviour {
	public bool local;
	public OutOfView view;
	public Transform PlayerTransform;
	public Transform Parent;
	// Use this for initialization
	void Start () {
		GetComponent<Renderer>().enabled = false;
		if (!local) {
			PlayerTransform = GameObject.FindGameObjectsWithTag("Player")[0].transform;
		} else {
			PlayerTransform = GameObject.FindGameObjectsWithTag("Player")[1].transform;
		}
		view = PlayerTransform.GetComponentInChildren<OutOfView>();
	}
	
	// Update is called once per frame
	void Update () {
		if(PlayerTransform == null) {
			if (!local) {
				PlayerTransform = GameObject.FindGameObjectsWithTag("Player")[0].transform;
			} else {
				PlayerTransform = GameObject.FindGameObjectsWithTag("Player")[1].transform;
			}
			view = PlayerTransform.GetComponentInChildren<OutOfView>();
		} else {
			Parent = transform.parent;
			transform.parent = null;
			transform.LookAt(PlayerTransform,Vector3.up);
			transform.parent = Parent;
			if (view != null && !view.vis) {
				GetComponent<Renderer>().enabled = true;
			} else {
				GetComponent<Renderer>().enabled = false;
			}
		}
	}
}
