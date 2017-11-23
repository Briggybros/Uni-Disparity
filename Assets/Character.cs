using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Uses WAD for moving and Space for jumping

public class Character : MonoBehaviour {

	private float RotationSpeed = 15.0f;
	private float MovementSpeed = 4.0f;
	private Vector3 pos;
	private Quaternion rot;
	public bool BlockInput;

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

	void Start () {
		rot = transform.localRotation;
		pos = transform.localPosition;
		BlockInput = false;
	}


	//Parents on interaction with collider
    void OnCollisionEnter(Collision c) {
        if (Parentables.Contains(c.gameObject)) {
            this.transform.SetParent(c.gameObject.transform, true);
            pos = this.transform.localPosition;
            rot = this.transform.localRotation;
            MovementSpeed = 0.8f;
            BlockInput = false;
        }
    }

    void OnCollisionExit(Collision c) {
        if (Parentables.Contains(c.gameObject)) {
            this.transform.parent = null;
            pos = this.transform.localPosition;
            rot = this.transform.localRotation;
            MovementSpeed = 4.0f;
        }
    }

    void Update () {
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
