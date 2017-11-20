using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingPlatformBehaviourScript: Receiver {
    public bool Lock;
    public bool Rotating;
    //public int Direction;
    public int Increment;
    public float Duration;
    private float TargetAngle;
    private float Facing;
    private float CurrentTime;
    private Vector3 SourceAxis;
    private Vector3 TargetAxis;


	// Use this for initialization
	protected override void Start () {
        CurrentTime = 0;
        TargetAngle = Increment;
        Quaternion sourceOrientation = this.transform.rotation;
        sourceOrientation.ToAngleAxis(out Facing, out SourceAxis);
        TargetAxis = transform.up;
    }
	
	// Update is called once per frame
	protected override void Update () {
        if (Rotating){
            Rotation();
        }else{
            if (!Lock) {

                Rotating = true;
            }
        }
	}

    protected void Rotation(){
        if (CurrentTime < Duration){
            CurrentTime += Time.deltaTime;
            float progress = CurrentTime / Duration;

            // Interpolate to get the current angle/axis between the source and target.
            float currentAngle = Mathf.Lerp(Facing, TargetAngle, progress);
            Vector3 currentAxis = Vector3.Slerp(SourceAxis, TargetAxis, progress);
            // Assign the current rotation
            this.transform.rotation = Quaternion.AngleAxis(currentAngle, TargetAxis);
        }
        else{
            Rotating = false;
            Quaternion sourceOrientation = this.transform.rotation;
            sourceOrientation.ToAngleAxis(out Facing, out SourceAxis);
            TargetAxis = transform.up;
            CurrentTime = 0;
            TargetAngle = Facing + Increment;
        }
    }

    protected void ToggleLock(){
        Lock = !Lock;
    }

    protected override void ColliderEnter(){
        ToggleLock();
    }

    protected override void ColliderExit(){
        ToggleLock();
    }

    protected override void PulseReceived() {
        ToggleLock();
    }

    protected override void SwitchReceived() {
        ToggleLock();
    }
}
