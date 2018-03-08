using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class JoystickCharacter : NetworkBehaviour

{

    private float RotationSpeed = 8.0f;
    private float MovementSpeed;
    public JoystickMovement joystick; 
    private Vector3 pos;
    private Quaternion rot;
    public bool BlockInput;
    public bool interacting;
    public bool touching;
    private int count;
    private NetworkIdentity targetNetworkIdent;
    private GameObject target;
    public bool canMove;
    private Vector3 cameraForwards;
    private Vector3 stickInput;
	private float fallMod = 2.5f;
	private float lowMod = 2f;
	private Rigidbody rb;
	private bool impetus = false;
	private bool jumpReq = false;
	private int impCount = 0;

    private Vector3 HeldScale;

    void Start()
    {
        joystick = GameObject.Find("Joystick").GetComponent<JoystickMovement>();
        rot = transform.localRotation;
        pos = transform.localPosition;
        BlockInput = false;
        interacting = false;
        touching = false;
        count = 0;
        targetNetworkIdent = null;// = this.GetComponent<NetworkIdentity>();
        rb = GetComponent<Rigidbody>();
    }

    [Command]
    void CmdsyncChange(string tag, GameObject target)
    {
        targetNetworkIdent.AssignClientAuthority(connectionToClient);
        RpcupdateState(tag, target);
        targetNetworkIdent.RemoveClientAuthority(connectionToClient);
    }

    [ClientRpc]
    void RpcupdateState(string tag, GameObject target)
    {
        target.tag = tag;
    }

    //Handles rotation
    IEnumerator Rotate(Quaternion finalRotation)
    {
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
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        rigidbody.velocity = Vector3.zero;
        rigidbody.angularVelocity = Vector3.zero;
        rigidbody.isKinematic = true;
        Transform activeCheckpointTransform = Checkpoint.GetActiveCheckpointTransform();
        transform.SetPositionAndRotation(activeCheckpointTransform.position, activeCheckpointTransform.rotation);
        rigidbody.isKinematic = false;
        if (BlockInput) BlockInput = false;
    }

    //Parents on interaction with collider
    void OnCollisionEnter(Collision c)
    {
        if (!(c.gameObject.GetComponent<RotatingPlatformBehaviourScript>() == null && c.gameObject.GetComponent<MovingPlatformBehaviour>() == null))
        {
            transform.SetParent(c.gameObject.transform.parent.transform, true);
            pos = transform.localPosition;
            rot = transform.localRotation;
            //MovementSpeed = 0.8f;
            BlockInput = false;
        }
        else if ((c.gameObject.GetComponent<Interactable>() != null))
        {
            targetNetworkIdent = c.gameObject.GetComponent<NetworkIdentity>();
            target = c.gameObject;
            touching = true;
        }
        if (c.gameObject.tag == "Enemy")
        {
            ResetPlayerToCheckpoint();
        }
    }

    void OnCollisionExit(Collision c)
    {
        if (!(c.gameObject.GetComponent<RotatingPlatformBehaviourScript>() == null && c.gameObject.GetComponent<MovingPlatformBehaviour>() == null))
        {
            transform.parent = null;
            pos = transform.localPosition;
            rot = transform.localRotation;
            //MovementSpeed = 4.0f;
        }
        else if ((c.gameObject.GetComponent<Interactable>() != null))
        {
            interacting = false;
            target = null;
            touching = false;
        }
    }

    static bool IsJump() {
#if UNITY_STANDALONE || UNITY_WEBGL || UNITY_EDITOR
		return Input.GetKeyDown(KeyCode.Space);
#elif UNITY_IOS || UNITY_ANDROID || UNITY_WP8 || UNITY_IPHONE || UNITY_EDITOR
		return Touch.Test("Jump");
#endif
    }

    void FixedUpdate() {
		if (jumpReq) {
			GetComponent<Rigidbody>().AddForce(Vector3.up * 7.0f, ForceMode.Impulse);
			jumpReq = false;
		}
		if (rb.velocity.y < 0) {
			rb.velocity += Vector3.up * Physics.gravity.y * (fallMod - 1) * Time.deltaTime;
		} else if (rb.velocity.y > 0 && !IsJump()) {
			rb.velocity += Vector3.up * Physics.gravity.y * (lowMod - 1) * Time.deltaTime;
		}
	}

    static bool isInteract()
    {
        #if UNITY_STANDALONE || UNITY_WEBGL || UNITY_EDITOR
        return Input.GetKeyDown(KeyCode.E);
        #elif UNITY_IOS || UNITY_ANDROID || UNITY_WP8 || UNITY_IPHONE || UNITY_EDITOR
        return Touch.Test("Use");
        #endif
    }
	
	// Update is called once per frame
	void Update () {

        if (!canMove)
        {
            return;
        }

        if (GetComponent<Rigidbody>().IsSleeping()) {
            GetComponent<Animator>().SetBool("Running", false);
        } else {
            GetComponent<Animator>().SetBool("Running", true);
        }

        if (!isLocalPlayer)
            return;

        cameraForwards = Vector3.ProjectOnPlane(Camera.main.transform.forward, Vector3.up);

       // Debug.Log("camera" + cameraForwards);

        transform.localRotation.eulerAngles.Set(0, transform.localRotation.eulerAngles.y, 0); //Force upright
        if (!interacting && isInteract() && touching)
        {
            interacting = true;
            //this.gameObject.tag = "PlayerInteract";
            CmdsyncChange("Bopped", target);
        }
        if (interacting && !isInteract())
        {
            interacting = false;
        }
        if (transform.localPosition.y <= -2)
        {
            ResetPlayerToCheckpoint();
        }

        //Rigidbody lines control jump start/end
        if (BlockInput && GetComponent<Rigidbody>().IsSleeping())
        {
            pos = transform.localPosition;
            rot = transform.localRotation;
            BlockInput = false;
        }

        if (!BlockInput)
        {
            pos = transform.localPosition;
            rot = transform.localRotation;
            stickInput = StickInput();
            if (stickInput != Vector3.zero)
            {   
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Quaternion.LookRotation(cameraForwards) * stickInput), Time.deltaTime * 8f);
                MovementSpeed = Vector3.Distance(joystick.centre, stickInput) * 6;
                pos +=  transform.rotation * Vector3.forward * 0.1f  * MovementSpeed;
                transform.localPosition = Vector3.MoveTowards(transform.localPosition, pos, Time.deltaTime * MovementSpeed);
            }

            if (!IsJump() && impCount > 60) {
				impetus = false;
			}
			if (IsJump() && !impetus) {
				//GetComponent<Rigidbody>().AddForce(Vector3.Scale((transform.forward + transform.up), new Vector3(6f, 6f, 6f)), ForceMode.Impulse);
				//GetComponent<Rigidbody>().velocity = Vector3.up * 7.0f;
				jumpReq = true;
				impetus = true;
				impCount = 0;
                //BlockInput = true;
            }
			impCount++;
        }

	}

    private Vector3 StickInput()
    {
        Vector3 dir = Vector3.zero;

        dir.x = joystick.Horizontal();
        dir.z = joystick.Vertical();

        if(dir.magnitude > 1)
        {
            dir.Normalize();
        }
        return dir;
    }
}
