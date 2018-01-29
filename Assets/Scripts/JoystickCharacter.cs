using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class JoystickCharacter : NetworkBehaviour
{

    private float RotationSpeed = 15.0f;
    private float MovementSpeed = 4.0f;
    public JoystickMovement joystick;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
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
