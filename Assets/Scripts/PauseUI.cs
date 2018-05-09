using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PauseUI : MonoBehaviour
{
  private NetworkDiscovery discovery;

  public void Start()
  {
    GameObject nm = GameObject.Find("Network Manager");
    discovery = nm.GetComponent<NetworkDiscovery>();
  }

  public void Disconnect()
  {
    if (discovery.running)
    {
      discovery.StopBroadcast();
    }
    else
    {
      NetworkManager.singleton.StopMatchMaker();
    }
    NetworkManager.singleton.StopHost();
    NetworkManager.singleton.onlineScene = null;
  }
}
