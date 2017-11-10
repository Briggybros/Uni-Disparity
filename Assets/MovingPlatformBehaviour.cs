using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class MovingPlatformBehaviour : Receiver{
    private bool Move, Switch;
    private int Forward;
    private int Stage;
    private Vector3 Target;
    public GameObject Anchor1, Anchor2;
    public int x, y, z;

    
    // Use this for initialization
    protected override void Start(){
        Move = false;
        Forward = -1;
        Stage = 1;
       // target = transform.position;
       // home = transform.position;
      //  target.y += 6;
    }

    private float BoolToFloat(bool Input) {
        if (Input) {
            return 1f;
        } else {
            return 0f;
        }
    }


    // Update is called once per frame
    protected override void Update(){
        if (Move){
            if (Forward == 1){
                Target = Anchor2.transform.position;
            }
            else{
                Target = Anchor1.transform.position;
            }
            switch (Stage){
                case 0:
                    Stage +=1;
                    if (Switch) {
                        ToggleForward();
                    }else {
                        Move = false;
                    }
                    break;
                case 1:
                    Target = Target - transform.position;
                    Target.x *= BoolToFloat(x == 1);
                    Target.y *= BoolToFloat(y == 1);
                    Target.z *= BoolToFloat(z == 1);
                    Target = Target + transform.position;

                    transform.position = Vector3.MoveTowards(transform.position, Target, 4 * Time.deltaTime);
                    if(transform.position == Target) { Stage+= Forward; }
                    break;
                case 2:
                    Target = Target - transform.position;
                    Target.x *= BoolToFloat(x == 2);
                    Target.y *= BoolToFloat(y == 2);
                    Target.z *= BoolToFloat(z == 2);
                    Target = Target + transform.position;

                    transform.position = Vector3.MoveTowards(transform.position, Target, 4 * Time.deltaTime);
                    if (transform.position == Target) { Stage += Forward; }
                    break;
                case 3:
                    Target = Target - transform.position;
                    Target.x *= BoolToFloat(x == 3);
                    Target.y *= BoolToFloat(y == 3);
                    Target.z *= BoolToFloat(z == 3);
                    Target = Target + transform.position;

                    transform.position = Vector3.MoveTowards(transform.position, Target, 4 * Time.deltaTime);
                    if (transform.position == Target) { Stage += Forward;}
                    break;
                case 4:
                    Stage -=1 ;
                    if (Switch) {
                        ToggleForward();
                    } else {
                        Move = false;
                    }
                    break;
            }
           
            //Open the door
        }
        else{
            //do whatever doors do while they wait
           // transform.position = Vector3.MoveTowards(transform.position, home, 4 * Time.deltaTime);
        }
    }

    protected void ToggleForward(){
        if(Forward == 1) {
            Forward = -1;
        }else {
            Forward = 1;
        }
    }

    protected override void ColliderEnter(){
        ToggleForward();
        if (!Move) { Move = true; }
    }

    protected override void PulseReceived() {
        ToggleForward();
        if (!Move) { Move = true; }
    }

    protected override void SwitchReceived() {
        ToggleForward();
        if (!Move) { Move = true; }
        Switch = !Switch;
    }

    protected override void ColliderExit(){
       // ToggleOpen();
    }
}
