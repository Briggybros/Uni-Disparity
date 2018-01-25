using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class DoorBehaviourScript : Receiver {


    public bool open;
    public Vector3 target,home;
	public int timer;


    protected void ToggleOpen(){
        open = !open;
    }

    protected override void ColliderEnter(){
        ToggleOpen();
    }

    protected override void ColliderExit(){
        ToggleOpen();
    }

    protected override void PulseReceived() { 
		StartCoroutine(TimerWait());
	}

    protected override void SwitchReceived() {
        ToggleOpen();
    }

	IEnumerator TimerWait() {
		ToggleOpen();
		yield return new WaitForSeconds(timer);
		ToggleOpen();
	}

    protected void init()
    {
        open = false;
        target = transform.position;
        home = transform.position;
    }
}
