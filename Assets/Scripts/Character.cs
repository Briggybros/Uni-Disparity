using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class Character : NetworkBehaviour {


	public float RotationSpeed = 15.0f;
	public float MovementSpeed = 6.0f;
	private Vector3 pos;
    private Vector3 clickPos;
	private Quaternion rot;
	private bool BlockInput;

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
        clickPos = transform.localPosition;
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
        if (!isLocalPlayer)
            return;
        this.transform.localRotation.eulerAngles.Set(0, this.transform.localRotation.eulerAngles.y, 0); //Force upright

		//Rigidbody lines control jump start/end
        if (BlockInput && this.GetComponent<Rigidbody>().IsSleeping()) {
            pos = this.transform.localPosition;
            rot = this.transform.localRotation;
            BlockInput = false;
        }
        if (!BlockInput) {
            pos = this.transform.localPosition;
            rot = this.transform.localRotation;

            //tap to move script
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Input.GetMouseButtonDown(0))
            {
                if (Physics.Raycast(ray, out hit, 100))
                {
                    if (hit.collider.CompareTag("Floor"))
                    {
                        clickPos = hit.point;
                        clickPos.y = this.transform.localPosition.y;
                    }
                }
            }

            //Old wad movement script
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

            //Deny y movement
            if (clickPos.y > this.transform.localPosition.y + 0.2f || clickPos.y < this.transform.localPosition.y - 0.2f) clickPos = this.transform.localPosition;
            //Update position
            if (Vector3.Distance(this.transform.position, clickPos) != 0)
            {
                this.transform.localPosition = Vector3.MoveTowards(
                    this.transform.localPosition,
                    clickPos,
                    Time.deltaTime * MovementSpeed
                    );
            }
            

            if (Input.GetKeyDown(KeyCode.Space)) {
                this.GetComponent<Rigidbody>().AddForce(Vector3.Scale((this.transform.forward + this.transform.up), new Vector3(6f, 6f, 6f)), ForceMode.Impulse);
                BlockInput = true;
            }
        }
	}
}
