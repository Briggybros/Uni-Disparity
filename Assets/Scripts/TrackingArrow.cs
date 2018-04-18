using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class TrackingArrow : NetworkBehaviour {
	public bool local;
	public OutOfView view;
	public Transform PlayerTransform;
	public Transform Parent;
	private Image dispImage;
	// Use this for initialization
	void Start () {
		dispImage = GetComponent<Image>();
		dispImage.color = new Color(255, 255, 255, 255);
		if (!local) {
			PlayerTransform = GameObject.FindGameObjectsWithTag("Player")[0].transform;
		} else {
			PlayerTransform = GameObject.FindGameObjectsWithTag("Player")[1].transform;
		}
		view = PlayerTransform.GetComponentInChildren<OutOfView>();
	}
	
	// Update is called once per frame
	void Update () {
		if (PlayerTransform == null) {
			if (!local) {
				PlayerTransform = GameObject.FindGameObjectsWithTag("Player")[0].transform;
			} else {
				PlayerTransform = GameObject.FindGameObjectsWithTag("Player")[1].transform;
			}
			view = PlayerTransform.GetComponentInChildren<OutOfView>();
		} else {
			Vector3 lookAtPos = new Vector3(PlayerTransform.position.x, transform.position.y, PlayerTransform.position.z);

			transform.LookAt(lookAtPos);
			if (view != null && !view.vis) {
				dispImage.color = new Color(255, 255, 255, 255);
			} else {
				dispImage.color = new Color(255, 255, 255, 0);
			}
		}
	}
}
