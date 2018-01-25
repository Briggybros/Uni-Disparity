using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractTrigger : CollisionTrigger {
	public bool interacted;
	protected virtual void Pressed() {
		interacted = true;
	}
}
