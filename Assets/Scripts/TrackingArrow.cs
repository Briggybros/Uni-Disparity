using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class TrackingArrow : NetworkBehaviour
{
  public bool local;
  public OutOfView view;
  public Transform PlayerTransform, Parent;

  private Image dispImage;
  private GameObject[] players;
  private Color Filled = new Color(255, 255, 255, 255), Transparent = new Color(255, 255, 255, 0);

  void Start()
  {
    dispImage = GetComponent<Image>();
    GetPlayers();
  }

  void Update()
  {
    if (PlayerTransform == null)
    {
      GetPlayers();
    }
    else
    {
      Vector3 lookAtPos = new Vector3(PlayerTransform.position.x, transform.position.y, PlayerTransform.position.z);

      transform.forward = lookAtPos;
      if (view != null && !view.vis)
      {
        dispImage.color = Filled;
      }
      else
      {
        dispImage.color = Transparent;
      }
    }
  }

  void GetPlayers()
  {
    players = GameObject.FindGameObjectsWithTag("Player");
    if (!local && players.Length > 0)
    {
      PlayerTransform = players[0].transform;
    }
    else if (players.Length > 1)
    {
      PlayerTransform = players[1].transform;
    }
    if (PlayerTransform != null)
    {
      view = PlayerTransform.GetComponentInChildren<OutOfView>();
    }
  }
}
