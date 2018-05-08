using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableBehaviour : MonoBehaviour {
	public int Increment;
	public float Duration;
	private float TargetAngle;
	private float CurrentTime;
	private Vector3 TargetAxis;
	public int count;
	// Use this for initialization

	void Start () {
		CurrentTime = 0;
		count = 0;
		TargetAngle = Increment;
		Quaternion sourceOrientation = this.transform.parent.rotation;
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
			float currentAngle = Mathf.Lerp(0, TargetAngle, progress);
			this.transform.parent.rotation = Quaternion.AngleAxis(currentAngle, TargetAxis);
		} else {
			Quaternion sourceOrientation = this.transform.parent.rotation;
			TargetAxis = transform.parent.up;
			CurrentTime = 0;
			TargetAngle = Increment;
		}
	}
}
