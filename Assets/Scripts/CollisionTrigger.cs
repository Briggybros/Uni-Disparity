using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionTrigger : Trigger
{
  public GameObject owner;
  public bool playerInteract;

  protected override void Start()
  {
    base.Start();
  }
  
  protected virtual void OnTriggerEnter(Collider other)
  {
    if (other.gameObject == owner || (playerInteract == true && other.CompareTag("Player")))
    {
      foreach (GameObject target in base.targets)
      {
        target.gameObject.GetComponent<ListenerScript>().BroadcastMessage("EnterFlag");
      }
      audioout.PlayOneShot(soundEffect);
    }
  }

  protected virtual void OnTriggerExit(Collider other)
  {
    if (other.gameObject == owner || (playerInteract == true && other.CompareTag("Player")))
    {
      foreach (GameObject target in base.targets)
      {
        target.gameObject.GetComponent<ListenerScript>().BroadcastMessage("ExitFlag");
      }
      audioout.PlayOneShot(soundEffect);
    }
  }

  protected virtual void OnTriggerStay(Collider other)
  {
    if (other.gameObject == owner || (playerInteract == true && other.CompareTag("Player")))
    {
      foreach (GameObject target in base.targets)
      {
        target.gameObject.GetComponent<ListenerScript>().BroadcastMessage("WithinFlag");
      }
    }
  }
}
