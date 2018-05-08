using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapdoorBehaviour : RotatingDoorBehaviour {

	public GameObject[] players;
	public GameObject heldPlayer;

	protected override void Start() {
		base.Start();
		players = GameObject.FindGameObjectsWithTag("Player");
		foreach (GameObject player in players) {
			if (player.GetComponent<JoystickCharacter>().isLocalPlayer) {
				heldPlayer = player;
			}
		}
	}

	protected override void Update() {
		base.Update();
		if (heldPlayer == null) {
			players = GameObject.FindGameObjectsWithTag("Player");
			foreach (GameObject player in players) {
				if (player.GetComponent<JoystickCharacter>().isLocalPlayer) {
					heldPlayer = player;
				}
			}
		}
	}

	protected override void ColliderEnter()
    {
		if (heldPlayer != null) {
			heldPlayer.BroadcastMessage("Trap", this.gameObject);
		}
	}

	protected override void ColliderWithin() {
	}

	protected override void ColliderExit()
    {
		if (heldPlayer != null) {
			heldPlayer.BroadcastMessage("UnTrap", this.gameObject);
		}
    }

    protected override void PulseReceived() {
        base.PulseReceived();
    }

    protected override void SwitchReceived() {
        base.SwitchReceived();
    }
}
