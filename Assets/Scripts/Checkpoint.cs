using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
  public bool activated = false;
  public static GameObject[] CheckpointList;

  void Start()
  {
    char World = CharacterPicker.GetWorld() == CharacterPicker.WORLDS.CAT ? 'A' : 'B';
    string tag = "Checkpoint" + World;
    CheckpointList = GameObject.FindGameObjectsWithTag(tag);
  }

  private void ActivateCheckpoint()
  {
    foreach (GameObject cp in CheckpointList)
    {
      cp.GetComponent<Checkpoint>().activated = false;
    }
    activated = true;
  }

  void OnTriggerEnter(Collider other)
  {
    if (other.gameObject.CompareTag("Player") && Array.Exists(CheckpointList, el => el == this.gameObject))
    {
      ActivateCheckpoint();
    }
  }

  public static Transform GetActiveCheckpointTransform()
  {
    Transform output = MyNetworkManager.singleton.GetStartPosition();
    if (CheckpointList != null)
    {
      foreach (GameObject cp in CheckpointList)
      {
        if (cp.GetComponent<Checkpoint>().activated)
        {
          output = cp.transform;
          break;
        }
      }
    }
    return output;
  }
}
