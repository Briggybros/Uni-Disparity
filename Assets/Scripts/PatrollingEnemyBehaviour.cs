using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrollingEnemyBehaviour : Receiver {
	public Vector3[] positions;

	private int destination = 0;
	private bool moved = true;
	private NavMeshAgent agent;

	// Use this for initialization
	protected override void Start () {
		agent = GetComponent<NavMeshAgent>();
	}

	protected override void Update() {
		if (moved && agent.velocity == Vector3.zero) {
			destination++;
			agent.destination = positions[destination % positions.Length];
			moved = false;
		} else {
			moved = true;
		}
	}
}
