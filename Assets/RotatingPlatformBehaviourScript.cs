using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingPlatformBehaviourScript: Receiver {
    public bool Lock;
    public bool Rotating;
    public int Direction;
    public int Increment;
    public int Speed;
    private int Facing;

    private int count;
    private int Quadrant;

	// Use this for initialization
	protected override void Start () {
        Quadrant = 0;
	}
	
	// Update is called once per frame
	protected override void Update () {
        if (Rotating){
            Rotate();
            int CurrentFace = (int)System.Math.Truncate(this.transform.localRotation.eulerAngles.y);
            if (Facing + Direction*90 == CurrentFace|| Facing + Direction*90 + 360 == CurrentFace ) {
                Rotating = false;
            }
        }else{
            if (!Lock) {
                Quadrant++;
                Quadrant = Quadrant % 4;
                Facing = (int) System.Math.Truncate(this.transform.localRotation.eulerAngles.y);
                Rotating = true;
            }
        }
	}

    protected void Rotate() {
        this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.Euler(0, Direction * Increment * Quadrant, 0), Time.deltaTime * Speed);
    }

    protected void ToggleLock()
    {
        Lock = !Lock;
    }

    protected override void ColliderEnter()
    {
        ToggleLock();
    }

    protected override void ColliderExit()
    {
        ToggleLock();
    }
}
