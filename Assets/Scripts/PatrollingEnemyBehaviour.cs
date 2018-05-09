using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrollingEnemyBehaviour : Receiver
{
  public Vector3[] positions;
	public float accuracy = 0.5f;

  private int destination = 0;  
  private NavMeshAgent agent;

  protected override void Start()
  {
    agent = GetComponent<NavMeshAgent>();
  }

  protected override void Update()
  {
    if (agent.enabled)
    {
      if (
        agent.remainingDistance != Mathf.Infinity &&
        agent.remainingDistance <= accuracy
      )
      {
        destination = (destination + 1) % (positions.Length);
        agent.destination = positions[destination];
      }
    }

    if (gameObject.transform.position.y <= -10)
    {
      Destroy(gameObject);
    }
  }
}
