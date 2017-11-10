using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

	private float RotationSpeed = 15.0f;
	private float MovementSpeed = 4.0f;
	Vector3 pos;
	Transform tr;
	Quaternion rot;

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
		tr = transform;
	}

	void Update () {
		if (tr.localRotation == rot && tr.position == pos) {
			// Left
			if (Input.GetKeyDown (KeyCode.D)) {
				StopAllCoroutines();
				rot = Quaternion.Euler (0, 90, 0) * transform.localRotation;
				StartCoroutine (Rotate (rot));
			}
			
			// Right
			if (Input.GetKeyDown (KeyCode.A)) {
				StopAllCoroutines();
				rot = Quaternion.Euler (0, -90, 0) * transform.localRotation;
				StartCoroutine (Rotate (rot));
			}

			// Up
			if (Input.GetKeyDown (KeyCode.W)) {
				Vector3 movement = transform.rotation * Vector3.forward;	
				pos += movement;
			}

			// Jump
			if (Input.GetKeyDown (KeyCode.Space)) {
				Vector3 movement = transform.rotation * Vector3.forward * 2;	
				pos += movement;
			}
		}

		transform.position = Vector3.MoveTowards(
			transform.position,
			pos,
			Time.deltaTime * MovementSpeed
		);
	}
}
