using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour {
    public GameObject target;
    public bool active;


    // Use this for initialization
    void Start()
    {
        active = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (active){
            active = false;
        }
    }

    public void Activate(bool set)
    {
        active = set;
    }
}
