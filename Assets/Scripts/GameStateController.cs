using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(NetworkIdentity))]
public class GameStateController : NetworkBehaviour {

    private static bool created = false;    
    private string level = "Menu";
    void Awake () {
        if (!created) {
            DontDestroyOnLoad(this.gameObject);
            created = true;
        }
        EventManager.AddEventListener("levelchange", OnLevelChange);
    }

    private void OnLevelChange(string newLevel) {
        level = newLevel;
    }
}
