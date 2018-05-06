using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class JoystickCharacter : NetworkBehaviour {

    private float RotationSpeed = 8.0f;
    private float MovementSpeed;
	private bool carrying = false;
    public JoystickMovement joystick; 
    private Vector3 pos;
    public bool interacting;
    public bool touching;
    public GameObject target;
    public bool canMove;

    private GameObject mark;
    private Vector3 cameraForwards;
    private Vector3 stickInput;
	private float fallMod = 2.5f;
	private float lowMod = 2f;
	private Rigidbody rb;
    public Animator pedantsAnimator;
	private bool impetus = false;
	private bool jumpReq = false;
	private int impCount = 0;
	private int grabLax;
	private Animator pedantsAnimator;
	public List<string> keys = new List<string>();
	public List<string> cores = new List<string>();

    private Vector3 HeldScale;

    void Start() {
        joystick = GameObject.Find("Joystick").GetComponent<JoystickMovement>();
        pos = transform.localPosition;
        interacting = false;
        touching = false;
        rb = GetComponent<Rigidbody>();
        pedantsAnimator = GetComponent<Animator>();
		grabLax = 0;
        mark = transform.GetChild(0).gameObject;
        mark.SetActive(false);

        if (!isLocalPlayer)
        {
            var colliders = GetComponentsInChildren<Collider>();
            foreach (var collider in colliders){
                collider.enabled = false;
            }
            var rigidbodys = GetComponentsInChildren<Rigidbody>();
            foreach (var rigidbody in rigidbodys){
                rigidbody.useGravity = false;
            }
        }
		pedantsAnimator = GetComponent<Animator>();
    }

	void Awake() {
		pedantsAnimator = GetComponent<Animator>();
	}

    [Command]
    void CmdsyncChange(string tag, GameObject target) {
        RpcupdateState(tag, target);
    }

	[Command]
	void CmdsyncNameChange(string name, GameObject target) {
		RpcupdateName(name, target);
	}

	[ClientRpc]
    void RpcupdateState(string tag, GameObject target) {
        target.tag = tag;
    }

	[ClientRpc]
	void RpcupdateName(string name,GameObject target) {
		target.name = target.name + name;
	}




    //Handles rotation
    IEnumerator Rotate(Quaternion finalRotation) {
        while (transform.localRotation != finalRotation)
        {
            transform.localRotation = Quaternion.Slerp(
                transform.localRotation,
                finalRotation,
                Time.deltaTime * RotationSpeed
            );
            yield return 0;
        }
        transform.localRotation = finalRotation;
    }
    void ResetPlayerToCheckpoint () {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.isKinematic = true;
        Transform activeCheckpointTransform = Checkpoint.GetActiveCheckpointTransform();
        transform.SetPositionAndRotation(activeCheckpointTransform.position, activeCheckpointTransform.rotation);
        rb.isKinematic = false;
    }

    //Parents on interaction with collider
    void OnCollisionEnter(Collision c) {
        if (c.gameObject.transform.parent.CompareTag("Bridge"))
        {
            transform.SetParent(c.gameObject.transform.parent.transform, true);
            pos = transform.localPosition;
        }
        if (c.gameObject.CompareTag("Enemy"))
        {
            ResetPlayerToCheckpoint();
        }
    }

	//Parents on interaction with collider
	void OnTriggerEnter(Collider c) {
		if ((c.gameObject.GetComponent<Interactable>() != null)) {
			target = c.gameObject;
			touching = true;
            //display exclamation mark
            mark.SetActive(true);
		}
		if (c.gameObject.CompareTag("Enemy")) {
			ResetPlayerToCheckpoint();
		}
	}

	void OnCollisionExit(Collision c) {
        if (c.gameObject.transform.parent.CompareTag("Bridge"))
        {
            transform.parent = null;
            pos = transform.localPosition;
        }
    }

	void OnTriggerExit(Collider c) {
		if ((c.gameObject.GetComponent<Interactable>() != null)) {
			interacting = false;
			target = null;
			touching = false;
            //hide exclamation mark
            mark.SetActive(false);
		}
	}

	void TargetSwitch(GameObject target) {
		if (isServer) {
			RpcHit(target);
		} else {
			CmdHit(target);
		}
	}

	[Command]
	void CmdHit(GameObject target) {
		target.GetComponent<ListenerScript>().BroadcastMessage("SwitchFlag");
	}

	[ClientRpc]
	void RpcHit(GameObject target) {
		CmdHit(target);
	}

	void syncAnima(bool running) {
		if (isServer) {
			RpcAnim(running);
		} else {
			CmdAnim(running);
		}
	}

	[Command]
	void CmdAnim(bool running) {
		pedantsAnimator.SetBool("Running", running);
	}

	[ClientRpc]
	void RpcAnim(bool running) {
		CmdAnim(running);
	}

	void Block(GameObject thingy) {
		if (isServer) {
			RpcBlocker(thingy);
		} else {
			CmdBlocker(thingy);
		}
	}

	void Unblock(GameObject thingy) {
		if (isServer) {
			RpcUnblocker(thingy);
		} else {
			CmdUnblock(thingy);
		}
	}

    void IncCount(GameObject thingy){
        if(isServer){
            RpcCount(thingy);
        }else{
            CmdCount(thingy);
        }
    }
    [Command]
    void CmdCount(GameObject thingy){
        RpcCount(thingy);
    }

    [ClientRpc]
    void RpcCount(GameObject thingy){
        thingy.GetComponent<PortalEnd>().count++;
    }


    void DecCount(GameObject thingy){
        if(isServer){
            RpcdCount(thingy);
        }else{
            CmddCount(thingy);
        }
    }
    [Command]
    void CmddCount(GameObject thingy){
        RpcdCount(thingy);
    }

    [ClientRpc]
    void RpcdCount(GameObject thingy){
        thingy.GetComponent<PortalEnd>().count--;
    }

	[Command]
	void CmdForceOwnership() {
		//targetNetworkIdent.AssignClientAuthority(connectionToClient);
	}

	[Command]
	void CmdBlocker( GameObject thingy) {
		RpcBlocker(thingy);
	}

	[Command]
	void CmdUnblock(GameObject thingy) {
		RpcUnblocker(thingy);
	}

	[ClientRpc]
	void RpcBlocker(GameObject target) {
		target.GetComponent<SlidingDoorBehaviour>().blocked = true;
		target.GetComponent<SlidingDoorBehaviour>().open = true;
	}


	[ClientRpc]
	void RpcUnblocker(GameObject target) {
		target.GetComponent<SlidingDoorBehaviour>().blocked = false;
		target.GetComponent<SlidingDoorBehaviour>().open = false;
	}

	void SyncAnim(bool running) {
		if (isServer) {
			RpcSyncAnim(gameObject, running);
		} else {
			CmdSyncAnim(gameObject, running);
		}
	}

	[Command]
	void CmdSyncAnim(GameObject character,bool running) {
		RpcSyncAnim(character,running);
	}

	[ClientRpc]
	void RpcSyncAnim(GameObject character,bool running) {
		pedantsAnimator.SetBool("Running", running);
	}

	static bool IsJump() {
#if UNITY_IOS || UNITY_ANDROID || UNITY_WP8 || UNITY_IPHONE || UNITY_EDITOR
		return Touch.Test("Jump");
#elif UNITY_STANDALONE || UNITY_WEBGL || UNITY_EDITOR
		return Input.GetKeyDown(KeyCode.Space);
#endif
    }

    void FixedUpdate() {
		if (jumpReq) {
			rb.AddForce(Vector3.up * 18.0f, ForceMode.Impulse);
			jumpReq = false;
		}
		if (rb.velocity.y < 0) {
			rb.velocity += Vector3.up * Physics.gravity.y * (fallMod - 1) * Time.deltaTime;
		} else if (rb.velocity.y > 0 && !IsJump()) {
			rb.velocity += Vector3.up * Physics.gravity.y * (lowMod - 1) * Time.deltaTime;
		}
	}

    static bool isInteract() {
#if UNITY_IOS || UNITY_ANDROID || UNITY_WP8 || UNITY_IPHONE || UNITY_EDITOR
        return Touch.Test("Use");
#elif UNITY_STANDALONE || UNITY_WEBGL || UNITY_EDITOR
        return Input.GetKeyDown(KeyCode.E);
#endif
    }
	
	// Update is called once per frame
	void Update () {

        if (!canMove) {
            return;
        }

        if (!isLocalPlayer)
            return;
		cameraForwards = Vector3.ProjectOnPlane(Camera.main.transform.forward, Vector3.up);

        // vibrate
        // if (isInteract() || IsJump()) {
        //     Handheld.Vibrate();
        // }

		grabLax++;
		if (isInteract() && touching && target.CompareTag("Key")) {
			keys.Add(target.name);
			CmdsyncChange("Bopped", target);
		//}else if(touching && target.GetComponent<PinMechanism>()){
            //target.tag = "Bopped";
        }else if(isInteract() && touching && target.GetComponent<TransportBehaviour>() != null) {
			foreach(string trans in cores) {
				CmdsyncNameChange(trans, target);
				cores.Remove(trans);
			}
		} else if (carrying && !interacting && isInteract() && grabLax > 20) {
			carrying = false;
			GameObject child = transform.GetComponentsInChildren<Interactable>()[0].gameObject;
			child.transform.SetParent(null);
			child.GetComponent<Rigidbody>().isKinematic = false;
			child.transform.Translate(0.0f, -0.4f, 0.0f);
			grabLax = 0;
		}
		else if(!interacting && isInteract() && touching && !carrying && target.CompareTag("Weight") && grabLax > 20) {
			carrying = true;
			target.transform.Translate(0.0f, 0.4f, 0.0f);
			target.GetComponent<Rigidbody>().isKinematic = true;
			target.transform.SetParent(transform);
			grabLax = 0;
		}
        else if (!interacting && isInteract() && touching && !carrying && !target.CompareTag("Weight")) {
            interacting = true;
			if (target.GetComponent<ChestBehaviour>() != null) {
				ChestBehaviour theirBe = target.GetComponent<ChestBehaviour>();
				string Name = theirBe.key.name;
				string Core = theirBe.gameObject.transform.GetChild(0).name;
				if (keys.Contains(Name)) {
					CmdsyncChange("Bopped", target);
				}
				if (!(cores.Contains(Core))) {
					cores.Add(Core);
				}
			} else {
				CmdsyncChange("Bopped", target);
			}
        }
        if (interacting && !isInteract()) {
            interacting = false;
        }
        if (transform.position.y <= -2) {
            ResetPlayerToCheckpoint();
        }

        pos = transform.localPosition;
        stickInput = StickInput();
        if (stickInput != Vector3.zero) {
            if (!animator.GetBool("Running")) {
                SyncAnim(true);
            }
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Quaternion.LookRotation(cameraForwards) * stickInput), Time.deltaTime * 8f);
            MovementSpeed = Vector3.Distance(joystick.centre, stickInput) * 30;
            pos +=  transform.rotation * Vector3.forward * 0.1f  * MovementSpeed;
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, pos, Time.deltaTime * MovementSpeed);
        } else {
            if (animator.GetBool("Running")) {
                SyncAnim(false);
            }
        }

        if (!IsJump() && impCount > 40) {
            impetus = false;
        }
        if (IsJump() && !impetus) {
            jumpReq = true;
            impetus = true;
            impCount = 0;
        }
        impCount++;
	}

    private Vector3 StickInput() {
        Vector3 dir = Vector3.zero;

        dir.x = joystick.Horizontal();
        dir.z = joystick.Vertical();

        dir.Normalize();
        return dir;
    }
}
