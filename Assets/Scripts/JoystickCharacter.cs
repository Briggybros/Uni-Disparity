using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class JoystickCharacter : NetworkBehaviour

{

    private float RotationSpeed = 15.0f;
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
            transform.position = Checkpoint.GetActiveCheckpointPosition();
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

    static bool IsJump()
    {
        #if UNITY_STANDALONE || UNITY_WEBGL || UNITY_EDITOR
        return Input.GetKeyDown(KeyCode.Space);
        #elif UNITY_IOS || UNITY_ANDROID || UNITY_WP8 || UNITY_IPHONE || UNITY_EDITOR
        return Touch.Test("Jump");
        #endif
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
            transform.position = Checkpoint.GetActiveCheckpointPosition();
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
                MovementSpeed = Vector3.Distance(joystick.centre, stickInput) * 10;
                pos +=  transform.rotation * Vector3.forward * 0.1f  * MovementSpeed;
                transform.localPosition = Vector3.MoveTowards(transform.localPosition, pos, Time.deltaTime * MovementSpeed);
            }

            if (IsJump())
            {
                GetComponent<Rigidbody>().AddForce(Vector3.Scale((transform.forward + transform.up), new Vector3(6f, 6f, 6f)), ForceMode.Impulse);
                BlockInput = true;
            }
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
