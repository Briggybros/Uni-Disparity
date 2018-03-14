using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrollingEnemyBehaviour : Receiver {
	public Vector3[] positions;
	public float height = 1.0f;
	public float baseOffset = 0.25f;
	public float radius = 1.0f;
	public float angularSpeed = 360.0f;
	public float positionalSpeed = 3.5f;

	private int destination = 0;
	private bool moved = false;
	private NavMeshAgent agent;

	// Use this for initialization
	protected override void Start () {
		this.agent = this.gameObject.AddComponent(typeof(NavMeshAgent)) as NavMeshAgent;
		this.agent.height = this.height;
		this.agent.radius = this.radius;
		this.agent.angularSpeed = this.angularSpeed;
		this.agent.baseOffset =	this.baseOffset;
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
