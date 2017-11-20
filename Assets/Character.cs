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

    void OnCollisionEnter(Collision c) {
        if (Parentables.Contains(c.gameObject)) {
           // HeldScale = this.transform.localScale;
            this.transform.SetParent(c.gameObject.transform, true);
            pos = this.transform.localPosition;
            //Debug.Log("COLLSIDE");
            rot = this.transform.localRotation;
            MovementSpeed = 0.8f;
            BlockInput = false;
           // this.transform.localScale = HeldScale;
        }
    }

    void OnCollisionExit(Collision c) {
        if (Parentables.Contains(c.gameObject)) {
            this.transform.parent = null;
            //this.transform.localPosition = new Vector3(4,-13,0);
            //this.transform.localRotation = Quaternion.identity; 
            pos = this.transform.localPosition;
            rot = this.transform.localRotation;
            MovementSpeed = 4.0f;
           // this.transform.localScale = HeldScale;
        }
    }

    void Update () {
        // the equality checks for rotation and position are overriden by the classes
        // to return equal when they are "close enough" accounting for precision
        // this introduces some error that can compound over time
        //rot = transform.localRotation;
        //transform.rotation.eulerAngles.Set(transform.localRotation.eulerAngles.x, transform.localRotation.eulerAngles.y, 0);
        this.transform.localRotation.eulerAngles.Set(0, this.transform.localRotation.eulerAngles.y, 0);
        if (BlockInput && this.GetComponent<Rigidbody>().IsSleeping()) {
            pos = this.transform.localPosition;
            rot = this.transform.localRotation;
            BlockInput = false;
        }
        if (!BlockInput) { 
            pos.y = this.transform.localPosition.y;
            pos = this.transform.localPosition;
            rot = this.transform.localRotation;
            if (Input.GetKey(KeyCode.D)/* && this.transform.localRotation == rot && this.transform.localPosition == pos*/) {
                StopAllCoroutines();
                rot *= Quaternion.Euler(0, 5, 0);
                StartCoroutine(Rotate(rot));
            }

            if (Input.GetKey(KeyCode.A)/* && this.transform.localRotation == rot && this.transform.localPosition == pos*/) {
                StopAllCoroutines();
                rot *= Quaternion.Euler(0, -5, 0);
                StartCoroutine(Rotate(rot));

            }

            if (Input.GetKey(KeyCode.W)/* && this.transform.localRotation == rot && this.transform.localPosition == pos*/) {
                //Debug.Log("Please Cat, please");
                pos += this.transform.localRotation * Vector3.forward * 0.1f * (MovementSpeed / 4);
            }

            this.transform.localPosition = Vector3.MoveTowards(
            this.transform.localPosition,
            pos,
            Time.deltaTime * MovementSpeed
            );

            if (Input.GetKeyDown(KeyCode.Space)/* this.transform.localRotation == rot &&*/) {
                //pos += this.transform.localRotation * Vector3.forward * 2 * (MovementSpeed / 4);
                this.GetComponent<Rigidbody>().AddForce(Vector3.Scale((this.transform.forward + this.transform.up), new Vector3(6f, 6f, 6f)), ForceMode.Impulse);
                BlockInput = true;
            }
        }
		

		
        Debug.Log("Pos" + pos + "::: actPos" + this.transform.localPosition);
        Debug.Log("Rot" + rot + "::: actRot" + this.transform.localRotation);
	}
}
