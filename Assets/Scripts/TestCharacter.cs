using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCharacter : MonoBehaviour {

	private float RotationSpeed = 15.0f;
	private float MovementSpeed = 25.0f;
	private Vector3 pos;
	private Quaternion rot;
	public bool BlockInput;
	public bool canMove;

	private Vector3 HeldScale;

	public List<GameObject> Parentables;

	//Handles rotation
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

	// Use this for initialization
	void Start () {
		rot = transform.localRotation;
		pos = transform.localPosition;
		BlockInput = false;
	}

	// Update is called once per frame
	void Update () {

		if (!canMove) {
			return;
		} 

		this.transform.localRotation.eulerAngles.Set(0, this.transform.localRotation.eulerAngles.y, 0); //Force upright

		//Rigidbody lines control jump start/end
		if (BlockInput && this.GetComponent<Rigidbody>().IsSleeping()) {
			pos = this.transform.localPosition;
			rot = this.transform.localRotation;
			BlockInput = false;
		}
		if (!BlockInput) { 
			pos.y = this.transform.localPosition.y;
			pos = this.transform.localPosition;
			rot = this.transform.localRotation;
			if (Input.GetKey(KeyCode.D)) {
				StopAllCoroutines();
				rot *= Quaternion.Euler(0, 5, 0);
				StartCoroutine(Rotate(rot));
			}

			if (Input.GetKey(KeyCode.A)) {
				StopAllCoroutines();
				rot *= Quaternion.Euler(0, -5, 0);
				StartCoroutine(Rotate(rot));

			}

			if (Input.GetKey(KeyCode.W)) {
				pos += this.transform.localRotation * Vector3.forward * 0.1f * (MovementSpeed / 4);
			}

			//Update position
			this.transform.localPosition = Vector3.MoveTowards(
				this.transform.localPosition,
				pos,
				Time.deltaTime * MovementSpeed
			);

			if (Input.GetKeyDown(KeyCode.Space)) {
				this.GetComponent<Rigidbody>().AddForce(Vector3.Scale((this.transform.forward + this.transform.up), new Vector3(6f, 6f, 6f)), ForceMode.Impulse);
				BlockInput = true;
			}
		}
	}
}