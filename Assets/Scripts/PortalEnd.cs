using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PortalEnd : Trigger
{
  public bool isTransition;
  public GameObject loadingPanel, end;
  public AudioClip loadingAudio;
  public int count;

  protected override void Start()
  {
    gameObject.GetComponent<Collider>().enabled = false;
    count = 0;
    base.Start();
  }

  protected void Update()
  {
    if (count > 1)
    {
      if (isTransition)
      {
        loadingPanel.SetActive(true);
        GameObject.Find("FXSource").GetComponent<AudioSource>().PlayOneShot(loadingAudio);
        NetworkManager.singleton.ServerChangeScene("Level 1");
      }
      else
      {
        end.GetComponent<ScoreboardController>().EndGame();
      }
      count = 0;
    }
    if (count < 0)
    {
      count = 0;
    }
  }

  protected virtual void OnTriggerEnter(Collider other)
  {
    if (other.CompareTag("Player") && this.gameObject != null)
    {
      other.gameObject.BroadcastMessage("IncCount", this.gameObject);
    }
  }

  protected virtual void OnTriggerExit(Collider other)
  {
    if (other.CompareTag("Player") && this.gameObject != null)
    {
      other.gameObject.BroadcastMessage("DecCount", this.gameObject);
    }
  }
}
