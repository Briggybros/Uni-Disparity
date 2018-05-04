using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(NetworkIdentity))]
public class GameStateController : NetworkBehaviour {
    
    [SyncVar]
    private string level = "Menu";
    void Awake () {
        DontDestroyOnLoad(gameObject);
    }

    void Start () {
        EventManager.AddEventListener("levelchange", OnLevelChange);
    }

    private void OnLevelChange(string newLevel) {
        level = newLevel;
    }
}
