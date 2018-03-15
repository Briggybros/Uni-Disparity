using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableBehaviour : MonoBehaviour {
	public int Increment;
	public float Duration;
	private float TargetAngle;
	private float Facing;
	private float CurrentTime;
	private Vector3 SourceAxis;
	private Vector3 TargetAxis;
	public int count;
	// Use this for initialization

	void Start () {
		CurrentTime = 0;
		count = 0;
		TargetAngle = Increment;
		Quaternion sourceOrientation = this.transform.parent.rotation;
		sourceOrientation.ToAngleAxis(out Facing, out SourceAxis);
		TargetAxis = transform.parent.up;
	}
	
	// Update is called once per frame
	void Update () {
		Rotation();
	}

	protected void Rotation() {
		if (CurrentTime < Duration) {
			CurrentTime += Time.deltaTime;
			float progress = CurrentTime / Duration;

			// Interpolate to get the current angle/axis between the source and target.
			float currentAngle = Mathf.Lerp(Facing, TargetAngle, progress);
			Vector3 currentAxis = Vector3.Slerp(SourceAxis, TargetAxis, progress);
			this.transform.parent.rotation = Quaternion.AngleAxis(currentAngle, TargetAxis);
		} else {
			Quaternion sourceOrientation = this.transform.parent.rotation;
			sourceOrientation.ToAngleAxis(out Facing, out SourceAxis);
			TargetAxis = transform.parent.up;
			CurrentTime = 0;
			TargetAngle = Facing + Increment;
		}
	}
}
