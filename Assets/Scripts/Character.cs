using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(Rigidbody))]
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
	public bool canMove;
	public bool interacting;
	public bool touching;
	private int count;
	private NetworkIdentity targetNetworkIdent;
	private GameObject target;
    private Vector3 HeldScale;

	//Handles rotation
	IEnumerator Rotate (Quaternion finalRotation) {
		while (transform.localRotation != finalRotation) {
			transform.localRotation = Quaternion.Slerp(
				transform.localRotation,
				finalRotation,
				Time.deltaTime * RotationSpeed
			);
			yield return 0;
		}
		transform.localRotation = finalRotation;
	}

	void Start () {
		rot = transform.localRotation;
		pos = transform.localPosition;
		BlockInput = false;
		canMove = true;
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
			transform.SetParent(c.gameObject.transform.parent.transform, true);
			pos = transform.localPosition;
			rot = transform.localRotation;
			//MovementSpeed = 0.8f;
			BlockInput = false;
		} else if ((c.gameObject.GetComponent<Interactable>() != null)) {
			targetNetworkIdent = c.gameObject.GetComponent<NetworkIdentity>();
			target = c.gameObject;
			touching = true;
		}
		if (c.gameObject.tag == "Enemy")
		{
			transform.position = Checkpoint.GetActiveCheckpointPosition();
		}
  }

    void OnCollisionExit(Collision c) {
		if (!(c.gameObject.GetComponent<RotatingPlatformBehaviourScript>() == null && c.gameObject.GetComponent<MovingPlatformBehaviour>() == null)) {
			transform.parent = null;
            pos = transform.localPosition;
            rot = transform.localRotation;
            //MovementSpeed = 4.0f;
        } else if ((c.gameObject.GetComponent<Interactable>() != null)) {
			interacting = false;
			target = null;
			touching = false;
		}
	}

	static bool isInteract() {
		#if UNITY_STANDALONE || UNITY_WEBGL || UNITY_EDITOR
		return Input.GetKeyDown(KeyCode.E);
		#elif UNITY_IOS || UNITY_ANDROID || UNITY_WP8 || UNITY_IPHONE || UNITY_EDITOR
		return Touch.Test("Use");
		#endif
	}

	static bool IsRight() {
		#if UNITY_STANDALONE || UNITY_WEBGL || UNITY_EDITOR
		return Input.GetKey(KeyCode.D);
		#elif UNITY_IOS || UNITY_ANDROID || UNITY_WP8 || UNITY_IPHONE || UNITY_EDITOR
		return Touch.Test("Right");
		#endif
	}

	static bool IsLeft() {
		#if UNITY_STANDALONE || UNITY_WEBGL || UNITY_EDITOR
		return Input.GetKey(KeyCode.A);
		#elif UNITY_IOS || UNITY_ANDROID || UNITY_WP8 || UNITY_IPHONE || UNITY_EDITOR
		return Touch.Test("Left");
		#endif
	}

	static bool IsForward() {
		#if UNITY_STANDALONE || UNITY_WEBGL || UNITY_EDITOR
		return Input.GetKey(KeyCode.W);
		#elif UNITY_IOS || UNITY_ANDROID || UNITY_WP8 || UNITY_IPHONE || UNITY_EDITOR
		return Touch.Test("Forward");
		#endif
	}

	static bool IsJump() {
		#if UNITY_STANDALONE || UNITY_WEBGL || UNITY_EDITOR
		return Input.GetKeyDown(KeyCode.Space);
		#elif UNITY_IOS || UNITY_ANDROID || UNITY_WP8 || UNITY_IPHONE || UNITY_EDITOR
		return Touch.Test("Jump");
		#endif
	}


    void Update () {

		if (!canMove) {
			return;
		}

        if (!isLocalPlayer)
            return;
		transform.localRotation.eulerAngles.Set(0, transform.localRotation.eulerAngles.y, 0); //Force upright
		if (!interacting && isInteract() && touching) {
			interacting = true;
			//this.gameObject.tag = "PlayerInteract";
			CmdsyncChange("Bopped",target);
		}
		if (interacting && !isInteract()) {
			interacting = false;
		}
		if (transform.localPosition.y <= -2)
        {
            transform.position = Checkpoint.GetActiveCheckpointPosition();
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
        if (BlockInput && GetComponent<Rigidbody>().IsSleeping()) {
            pos = transform.localPosition;
            rot = transform.localRotation;
            BlockInput = false;
        }
        if (!BlockInput) {
            pos.y = transform.localPosition.y;
            pos = transform.localPosition;
            rot = transform.localRotation;
            if (IsRight()) {
                StopAllCoroutines();
                rot *= Quaternion.Euler(0, 5, 0);
                StartCoroutine(Rotate(rot));
            }

            if (IsLeft()) {
                StopAllCoroutines();
                rot *= Quaternion.Euler(0, -5, 0);
                StartCoroutine(Rotate(rot));

            }

            if (IsForward()) {
                pos += transform.localRotation * Vector3.forward * 0.1f * (MovementSpeed / 4);
            }

			//Update position
            transform.localPosition = Vector3.MoveTowards(
            transform.localPosition,
            pos,
            Time.deltaTime * MovementSpeed
            );

            if (IsJump()) {
                GetComponent<Rigidbody>().AddForce(Vector3.Scale((transform.forward + transform.up), new Vector3(6f, 6f, 6f)), ForceMode.Impulse);
                BlockInput = true;
            }
        }
	}
}