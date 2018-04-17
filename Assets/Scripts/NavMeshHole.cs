using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshHole : MonoBehaviour {
	public bool initiallyHoley = false;

	private BoxCollider box;

	void Start() {
		box = GetComponent<BoxCollider>();
		box.enabled = initiallyHoley;
	}

	public void EnableHole() {
		box.enabled = true;
	}

	public void DisableHole() {
		box.enabled = false;
	}

	public void Toggle() {
		box.enabled = !box.enabled;
	}

	public void OnCollisionEnter(Collision collision) {
		NavMeshAgent agent = collision.gameObject.GetComponent<NavMeshAgent>();
		Rigidbody body = collision.gameObject.GetComponent<Rigidbody>();
		if (agent != null && body != null) {
			agent.enabled = false;
			body.isKinematic = true;
			body.useGravity = true;
		}
	}
}
