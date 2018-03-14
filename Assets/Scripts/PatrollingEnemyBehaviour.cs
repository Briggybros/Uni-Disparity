using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrollingEnemyBehaviour : Receiver {
	public Vector3[] positions;

	private int destination = 0;
	private bool moved = false;
	private NavMeshAgent agent;

	// Use this for initialization
	protected override void Start () {
		this.agent = this.gameObject.AddComponent(typeof(NavMeshAgent)) as NavMeshAgent;
		this.agent.height = 1;
		this.agent.radius = 1;
		this.agent.angularSpeed = 360;
		this.agent.baseOffset =	this.agent.height / 4;
		Debug.Log(this.destination);
	}

	protected override void Update() {
		if (moved && this.agent.velocity == Vector3.zero) {
			this.destination++;
			this.agent.destination = this.positions[this.destination % this.positions.Length];
			this.moved = false;
		} else {
			this.moved = true;
		}
	}
}
