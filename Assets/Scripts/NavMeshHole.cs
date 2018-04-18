using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshHole : MonoBehaviour {
	public bool isActive = false;

	public void Toggle() {
		isActive = !isActive;
	}

	public void OnTriggerEnter(Collider collider) {
		if (isActive) {
			NavMeshAgent agent = collider.gameObject.GetComponent<NavMeshAgent>();
			Rigidbody body = collider.gameObject.GetComponent<Rigidbody>();
			if (agent.enabled && agent != null && body != null) {
				Vector3 vel = agent.velocity;
				agent.enabled = false;
				body.useGravity = true;
				body.velocity = new Vector3(vel.x, vel.y + 2f, vel.z);
				body.constraints = RigidbodyConstraints.None;
				BoxCollider[] colliders = collider.gameObject.GetComponents<BoxCollider>();
				foreach (BoxCollider c in colliders) {
					c.enabled = false;
				}
			}
		}
	}
}
