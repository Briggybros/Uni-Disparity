using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Trigger : NetworkBehaviour
{
  public bool requiresInteract;
  public GameObject[] targets;

  public AudioClip soundEffect;
  protected AudioSource audioout;

  protected virtual void Start()
  {
    audioout = GameObject.Find("FXSource").GetComponent<AudioSource>();
  }
}
