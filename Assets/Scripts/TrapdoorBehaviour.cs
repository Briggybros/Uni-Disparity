using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapdoorBehaviour : RotatingDoorBehaviour {
    protected override void ColliderEnter()
    {
        base.ColliderEnter();
        NavMeshHole hole = this.gameObject.GetComponent(typeof(NavMeshHole)) as NavMeshHole;
        hole.Toggle();
    }

    protected override void ColliderExit()
    {
        base.ColliderEnter();
        NavMeshHole hole = this.gameObject.GetComponent(typeof(NavMeshHole)) as NavMeshHole;
        hole.Toggle();
    }

    protected override void PulseReceived() {
        base.ColliderEnter();
        NavMeshHole hole = this.gameObject.GetComponent(typeof(NavMeshHole)) as NavMeshHole;
        hole.Toggle();
    }

    protected override void SwitchReceived() {
        base.ColliderEnter();
        NavMeshHole hole = this.gameObject.GetComponent(typeof(NavMeshHole)) as NavMeshHole;
        hole.Toggle();
    }
}
