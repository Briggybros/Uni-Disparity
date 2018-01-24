using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class Character : NetworkBehaviour {


	private float RotationSpeed = 15.0f;
	private float MovementSpeed = 4.0f;
	private Vector3 pos;
	private Quaternion rot;
	public bool BlockInput;
	public bool interacting;
	public bool touching;
	private int count;
	private NetworkIdentity targetNetworkIdent;
	private GameObject target;
  private Vector3 HeldScale;

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
		interacting = false;
		touching = false;
		count = 0;
		targetNetworkIdent = null;// = this.GetComponent<NetworkIdentity>();
	}

	[Command]
	void CmdsyncChange(string tag, GameObject target) {
		targetNetworkIdent.AssignClientAuthority(connectionToClient);
		RpcupdateState(tag,target);
		targetNetworkIdent.RemoveClientAuthority(connectionToClient);
	}

	[ClientRpc]
	void RpcupdateState(string tag, GameObject target) {
		target.tag = tag;
	}

	//Parents on interaction with collider
	void OnCollisionEnter(Collision c) {
		if (!(c.gameObject.GetComponent<RotatingPlatformBehaviourScript>() == null && c.gameObject.GetComponent<MovingPlatformBehaviour>() == null)) {
			this.transform.SetParent(c.gameObject.transform.parent.transform, true);
			pos = this.transform.localPosition;
			rot = this.transform.localRotation;
			//MovementSpeed = 0.8f;
			BlockInput = false;
		}else if ((c.gameObject.GetComponent<Interactable>() != null)) {
			targetNetworkIdent = c.gameObject.GetComponent<NetworkIdentity>();
			target = c.gameObject;
			touching = true;
		}
		if(c.gameObject.tag == "Enemy")
		{
			this.transform.position = Checkpoint.GetActiveCheckpointPosition();
		}
  }

    void OnCollisionExit(Collision c) {
		if (!(c.gameObject.GetComponent<RotatingPlatformBehaviourScript>() == null && c.gameObject.GetComponent<MovingPlatformBehaviour>() == null)) {
			this.transform.parent = null;
            pos = this.transform.localPosition;
            rot = this.transform.localRotation;
            //MovementSpeed = 4.0f;
        } else if ((c.gameObject.GetComponent<Interactable>() != null)) {
			interacting = false;
			target = null;
			touching = false;
		}
	}


    void Update () {
        if (!isLocalPlayer)
            return;
				this.transform.localRotation.eulerAngles.Set(0, this.transform.localRotation.eulerAngles.y, 0); //Force upright
				if (Input.GetKeyDown(KeyCode.E) && touching) {
					interacting = true;
					//this.gameObject.tag = "PlayerInteract";
					CmdsyncChange("Bopped",target);
				}
				if (this.transform.localPosition.y <= -2)
        {
            this.transform.position = Checkpoint.GetActiveCheckpointPosition();
        }
		/*if (interacting) {
			count++;
			if(count > 2) {
				//this.gameObject.tag = "Player";
				CmdsyncChange("Player",target);
				count = 0;
			}
		}*/
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
