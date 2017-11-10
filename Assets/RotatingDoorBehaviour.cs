using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingDoorBehaviour : DoorBehaviourScript {

    private bool Turning;
    public float MinRot, MaxRot;

    private float Duration;
    private float TargetAngle;
    private float Facing;
    private float CurrentTime;
    private Vector3 SourceAxis;
    private Vector3 TargetAxis;

    protected override void Start()
    {
        init();
        CurrentTime = 0f;
        Duration = 1f;
        Quaternion sourceOrientation = this.transform.parent.rotation;
        sourceOrientation.ToAngleAxis(out Facing, out SourceAxis);
        TargetAxis = transform.parent.up;
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (open)
        {
            TargetAngle = MaxRot;
            if (Turning) {
                Rotation();
            }
            
            //transform.position = Vector3.MoveTowards(transform.position, target, 4 * Time.deltaTime);
            //Open the door
        }
        else
        {
            TargetAngle = MinRot;
            if (Turning) {
                Rotation();
            }
            
            //do whatever doors do while they wait
            //transform.position = Vector3.MoveTowards(transform.position, home, 4 * Time.deltaTime);
        }
    }

    protected override void ColliderEnter()
    {
        ToggleOpen();
        Turning = true;
    }

    protected override void ColliderExit()
    {
        ToggleOpen();
        Turning = true;
    }

    protected void Rotation()
    {
        if (CurrentTime < Duration)
        {
            CurrentTime += Time.deltaTime;
            float progress = CurrentTime / Duration;

            // Interpolate to get the current angle/axis between the source and target.
            float currentAngle = Mathf.Lerp(Facing, TargetAngle, progress);
            Vector3 currentAxis = Vector3.Slerp(SourceAxis, TargetAxis, progress);
            // Assign the current rotation
            this.transform.parent.rotation = Quaternion.AngleAxis(currentAngle, TargetAxis);
        }
        else
        {
            Turning = false;
            Quaternion sourceOrientation = this.transform.parent.rotation;
            sourceOrientation.ToAngleAxis(out Facing, out SourceAxis);
            TargetAxis = transform.parent.up;
            CurrentTime = 0;
        }
    }
}
