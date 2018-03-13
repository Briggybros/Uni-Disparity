using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FollowTarget : MonoBehaviour {

	
	private List<Transform> targets = new List<Transform>();
	public float smoothSpeed = 0.125f;
	public GameObject playerPrefab;
	public GameObject[] playerList;

	void start(){
		if (playerList == null){
			playerList = GameObject.FindGameObjectsWithTag("Player");
			if (playerList == null) Debug.Log("fuck this");
		}

		foreach (GameObject player in playerList) {
			Debug.Log("add player");
			targets.Add(player.transform);
		}
	}
	
	void LateUpdate(){
		// transform.position = target.position;
		if (!targets.Any()){
			Debug.Log("rip, didn't work");
			return;
		}
		transform.position = targets[0].position;
	}

}
