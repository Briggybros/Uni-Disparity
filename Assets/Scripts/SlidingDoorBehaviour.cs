using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SlidingDoorBehaviour : DoorBehaviourScript
{
  public float OpenHeight;
  public int DownSpeed, UpSpeed;
  public bool looping, mirror;
  [SyncVar]
  public bool blocked;
  public GameObject[] players;
  public GameObject heldPlayer;

  protected override void Start()
  {
    init();
    target.y += OpenHeight;
    if (looping)
    {
      Looping();
    }
    players = GameObject.FindGameObjectsWithTag("Player");
    foreach (GameObject player in players)
    {
      if (player.GetComponent<JoystickCharacter>().isLocalPlayer)
      {
        heldPlayer = player;
      }
    }
  }

  protected override void Update()
  {
    if (heldPlayer == null)
    {
      players = GameObject.FindGameObjectsWithTag("Player");
      foreach (GameObject player in players)
      {
        if (player.GetComponent<JoystickCharacter>().isLocalPlayer)
        {
          heldPlayer = player;
        }
      }
    }
    if (!(mirror && !isServer))
    {
      if (open)
      {
        transform.position = Vector3.MoveTowards(transform.position, target, DownSpeed * Time.deltaTime);
      }
      else
      {
        transform.position = Vector3.MoveTowards(transform.position, home, UpSpeed * Time.deltaTime);
      }
    }
  }

  protected override void ColliderWithin()
  {
    if (heldPlayer != null)
    {
      heldPlayer.BroadcastMessage("Block", this.gameObject);
    }
  }

  protected override void ColliderExit()
  {
    if (heldPlayer != null)
    {
      heldPlayer.BroadcastMessage("Unblock", this.gameObject);
    }
  }
}
