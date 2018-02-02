using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.AI;

[RequireComponent(typeof(NetworkIdentity))]
[RequireComponent(typeof(Rigidbody))]
public class Character : NetworkBehaviour
{
    private Vector3 pos;
    private Quaternion rot;
    private NetworkIdentity targetNetworkIdent;
    private float yLevel;
    public bool canMove;

    private Vector3 HeldScale;

    void Start() {
        rot = transform.localRotation;
        pos = transform.localPosition;
        canMove = true;
        targetNetworkIdent = GetComponent<NetworkIdentity>();
    }

    [Command]
    void CmdsyncChange(string tag, GameObject target) {
        Debug.Log("csc: " + tag + ", " + target);
        targetNetworkIdent.AssignClientAuthority(connectionToClient);
        RpcupdateState(tag, target);
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
        }
        if ((c.gameObject.GetComponent<Interactable>() != null)) {
            CmdsyncChange("Bopped", c.gameObject);
        }
        if (c.gameObject.tag == "Enemy") {
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
            CmdsyncChange("Static", c.gameObject);
        }
    }

    void Update() {
        if (!canMove) {
            return;
        }

        if (!isLocalPlayer)
            return;
        transform.localRotation.eulerAngles.Set(0, transform.localRotation.eulerAngles.y, 0); //Force upright
        
        if (transform.localPosition.y <= -2) {
            transform.position = Checkpoint.GetActiveCheckpointPosition();
        }
        //Falling check
        if (this.transform.localPosition.y <= yLevel - 2) {
            this.transform.SetPositionAndRotation(Checkpoint.GetActiveCheckpointPosition(), Quaternion.identity);
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Input.GetMouseButtonDown(0)) {
            if (Physics.Raycast(ray, out hit, 200)) {
                this.GetComponent<NavMeshAgent>().destination = hit.point;
            }
        }

        if (this.GetComponent<NavMeshAgent>().velocity != Vector3.zero) {
            this.GetComponent<Animator>().SetBool("Running", true);
        } else {
            this.GetComponent<Animator>().SetBool("Running", false);
        }
    }
}