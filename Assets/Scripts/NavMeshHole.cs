using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshHole : MonoBehaviour {
	public Vector3 center;
	public Vector3 size;
	public bool initiallyHoley = false;

	private BoxCollider box;

	void Start() {
		box = this.gameObject.AddComponent(typeof(BoxCollider)) as BoxCollider;
		box.center = center;
		box.size = size;
		box.isTrigger = true;
		box.enabled = initiallyHoley;
	}

	public void EnableHole() {
		box.enabled = true;
	}

	public void DisableHole() {
		box.enabled = false;
	}

	public void OnCollisionEnter(Collision collision) {
		NavMeshAgent agent = collision.gameObject.GetComponent(typeof(NavMeshAgent)) as NavMeshAgent;
		Rigidbody body = collision.gameObject.GetComponent(typeof(Rigidbody)) as Rigidbody;
		if (agent != null && body != null) {
			agent.enabled = false;
			body.isKinematic = true;
		}
	}
}
