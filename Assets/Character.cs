using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Uses WAD for moving and Space for jumping

public class Character : MonoBehaviour {

	private float RotationSpeed = 15.0f;
	private float MovementSpeed = 4.0f;
	private Vector3 pos;
	private Quaternion rot;

	IEnumerator Rotate(Quaternion finalRotation){
		while(this.transform.localRotation != finalRotation) {
			this.transform.localRotation = Quaternion.Slerp(
				this.transform.localRotation,
				finalRotation,
				Time.deltaTime * RotationSpeed
			);
			yield return 0;
		}
		this.transform.localRotation = finalRotation;
	}

	void Start () {
		rot = transform.localRotation;
		pos = transform.position;
	}

    private void OnCollisionEnter(Collision c) {
        if (c.transform.name == "Spinner") {
            this.transform.parent = c.transform;
        }
    }

    private void OnCollisionExit(Collision c) {
        if (c.transform.name == "Spinner") {
            this.transform.parent = null;
        }
    }

    void Update () {
        // the equality checks for rotation and position are overriden by the classes
        // to return equal when they are "close enough" accounting for precision
        // this introduces some error that can compound over time
        //rot = transform.localRotation;
        pos.y = this.transform.position.y;
        if (Input.GetKeyDown (KeyCode.D) && this.transform.localRotation == rot && this.transform.position == pos) {
			StopAllCoroutines ();
			rot *= Quaternion.Euler (0, 90, 0);
			StartCoroutine (Rotate (rot));
		}

		if (Input.GetKeyDown (KeyCode.A) && this.transform.localRotation == rot && this.transform.position == pos) {
			StopAllCoroutines ();
			rot *= Quaternion.Euler (0, -90, 0);
			StartCoroutine (Rotate (rot));

		}

		if (Input.GetKeyDown (KeyCode.W) && this.transform.localRotation == rot && this.transform.position == pos) {
			pos += this.transform.localRotation * Vector3.forward;
		}

		if (Input.GetKeyDown (KeyCode.Space) && this.transform.localRotation == rot && this.transform.position == pos) {
			pos += this.transform.localRotation * Vector3.forward * 2;
		}

		this.transform.position = Vector3.MoveTowards (
			this.transform.position,
			pos,
			Time.deltaTime * MovementSpeed
		);
	}
}
