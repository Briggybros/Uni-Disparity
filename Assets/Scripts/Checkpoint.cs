using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {

    public bool activated = false;
    public static GameObject[] CheckpointList;


    // Use this for initialization
    void Start () {

        CheckpointList = GameObject.FindGameObjectsWithTag("Checkpoint");
		
	}

    private void ActivateCheckpoint() //Activated current, deactivate all else
    {
        foreach (GameObject cp in CheckpointList)
        {
            cp.GetComponent<Checkpoint>().activated = false;
            //cp.GetComponent().SetBool("Active", false);
        }
        activated = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            ActivateCheckpoint();
        }
    }

    public static Vector3 GetActiveCheckpointPosition()
    {
        Vector3 output = new Vector3(0, 0, 0);
        if(CheckpointList != null)
        {
            foreach (GameObject cp in CheckpointList)
            {
                if (cp.GetComponent<Checkpoint>().activated)
                {
                    output = cp.transform.position;
                    break;
                }
            }
        }
        return output;
    }

    // Update is called once per frame
    void Update () {
		
	}
}
