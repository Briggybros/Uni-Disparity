using UnityEngine;
using System.Collections.Generic;
using Vuforia;

[RequireComponent(typeof (VuforiaBehaviour))]
public class SmoothCamera : MonoBehaviour {

	public int smoothingFrames = 10;

	private Quaternion smoothedRotation;
	private Vector3 smoothedPosition;

	private Queue<Quaternion> rotations;
	private Queue<Vector3> positions;

	public void OnTrackablesUpdated() {

		if (rotations.Count >= smoothingFrames) {
			rotations.Dequeue();
			positions.Dequeue();
		}

		rotations.Enqueue(transform.rotation);
		positions.Enqueue(transform.position);

		Vector4 avgr = Vector4.zero;
		foreach (Quaternion singleRotation in rotations) {
			AverageQuaternion(ref avgr, singleRotation, rotations.Peek(), rotations.Count);
		}

		Vector3 avgp = Vector3.zero;
		foreach (Vector3 singlePosition in positions) {
			avgp += singlePosition;
		}
		avgp /= positions.Count;

		smoothedRotation = new Quaternion(avgr.x, avgr.y, avgr.z, avgr.w);
		smoothedPosition = avgp;
	}

	void OnInitialized() {
		
	}

	// Use this for initialization
	void Start () {

		rotations = new Queue<Quaternion>(smoothingFrames);
		positions = new Queue<Vector3>(smoothingFrames);

		VuforiaARController vuforia = VuforiaARController.Instance;

		vuforia.RegisterVuforiaStartedCallback(OnInitialized);
		vuforia.RegisterTrackablesUpdatedCallback (OnTrackablesUpdated);
	}

	// Update is called once per frame
	void LateUpdate () {
		transform.rotation = smoothedRotation;
		transform.position = smoothedPosition;
	}

	private void AverageQuaternion(ref Vector4 cumulative, Quaternion newRotation, Quaternion firstRotation, int addAmount) {
        
        //Before we add the new rotation to the average (mean), we have to check whether the quaternion has to be inverted. Because
        //q and -q are the same rotation, but cannot be averaged, we have to make sure they are all the same.
        if(!AreQuaternionsClose(newRotation, firstRotation)){
            newRotation = InverseSignQuaternion(newRotation); 
        }
        
        //Average the values
        cumulative.w += newRotation.w;
        cumulative.x += newRotation.x;
        cumulative.y += newRotation.y;
        cumulative.z += newRotation.z;
        
        //note: if speed is an issue, you can skip the normalization step
        //return NormalizeQuaternion(x, y, z, w);
    }

	private bool AreQuaternionsClose(Quaternion q1, Quaternion q2){
		return Quaternion.Dot(q1, q2) >= 0.0f;
    }

	private Quaternion InverseSignQuaternion(Quaternion q){
		return new Quaternion(-q.x, -q.y, -q.z, -q.w);
    }

}