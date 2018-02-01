using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrollingEnemyBehaviour : Receiver {
	private bool Move;
	private int Forward,Move_Forward;
	private int Move_Index,Target_index;
	private int Stage,TempStage;
	private Vector3 Target;
	private Vector3 TempTarget;
	private int Orientation;
	private Vector3 TargetDir;
	private Vector3 Order;
	public GameObject[] Waypoints;
	public Vector3[] Directions;
	public bool Circular;


	// Use this for initialization
	protected override void Start () {
		Move = true;
		Forward = 1;
		Move_Forward = 1;
		Move_Index = 0;
		Target_index = 1;
		Stage = 1;
		TempStage = 0;
		Target = Waypoints[Target_index].transform.position;
		Order = Directions[Move_Index];
	}
	protected virtual float BoolToFloat(bool Input) {
        if (Input) {
            return 1f;
        } else {
            return 0f;
        }
    }
	// Update is called once per frame
	protected override void Update () {
		if(Move){
			TempTarget = Target;
			if(TempStage != Stage && Stage != 0 && Stage != 4){
				TempTarget = Target - transform.position;
				TempTarget.x *= BoolToFloat(Order.x == Stage);
				TempTarget.y *= BoolToFloat(Order.y == Stage);
				TempTarget.z *= BoolToFloat(Order.z == Stage);
				if(TempTarget.magnitude < 1){
					TempTarget.y = Stage;
				}
				if(TempTarget.x > 0 || TempTarget.z > 0){
					Orientation = 1;
				}else{
					Orientation = -1;
				}
				TempTarget.x = BoolToFloat(Order.x == Stage);
				TempTarget.y = BoolToFloat(Order.y == Stage);
				TempTarget.z = BoolToFloat(Order.z == Stage);
				//Orientation = transform.rotation;
				TempTarget  *= Orientation;
				if(TempTarget.y != 0 ){
					TempStage = Stage;
				}else{
					if((TempTarget-transform.forward).magnitude < 0.05){
						TempStage = Stage;
					}else{
						transform.rotation = Quaternion.Slerp(
							transform.rotation,
							Quaternion.LookRotation(TempTarget),
							Time.deltaTime * 2f
						);
					}
				}
			}else{
				Stage = Movement(Stage,TempTarget,Order,Forward);
				//Stage+=1;
			}
		}else{
			Move_Index += Move_Forward;
			Target_index += Forward;
			if(Move_Index == Directions.Length){
				if(Circular){
					Move_Index = 0;
					Order = Directions[Move_Index];
				}else{
					Move_Forward = -1;
					Move_Index -= 1;
					Order = Directions[Move_Index];
				}
			}else if(Move_Index == -1){
				Move_Forward = 1;
				Move_Index = 0;
				Order = Directions[Move_Index];
			}else{
				Order = Directions[Move_Index];
			}
			if(Target_index == Waypoints.Length){
				if(Circular){
					Target_index = 0;
					Target = Waypoints[Target_index].transform.position;
				}else{
					Forward = -1;
					Target_index -= 1;
					Target = Waypoints[Target_index].transform.position;
				}
			}else if(Target_index == -1){
				Forward = 1;
				Target_index = 0;
				Target = Waypoints[Target_index].transform.position;
			}else{
				Target = Waypoints[Target_index].transform.position;
			}
			if(Forward == 1){
				Stage = 1;
			}else{
				Stage = 3;
			}
			Move = true;
		}
	}
	int Movement(int Stage,Vector3 Target,Vector3 Order,int Forward){
		switch (Stage){
			case 0:
				Move = false;
				break;
			case 1:
				Target = Target - transform.position;
				Target.x *= BoolToFloat(Order.x == 1);
				Target.y *= BoolToFloat(Order.y == 1);
				Target.z *= BoolToFloat(Order.z == 1);
				Target = Target + transform.position;

				transform.position = Vector3.MoveTowards(transform.position, Target, 4 * Time.deltaTime);
				if(transform.position == Target) { Stage+= Forward; }
				break;
			case 2:
				Target = Target - transform.position;
				Target.x *= BoolToFloat(Order.x == 2);
				Target.y *= BoolToFloat(Order.y == 2);
				Target.z *= BoolToFloat(Order.z == 2);
				Target = Target + transform.position;

				transform.position = Vector3.MoveTowards(transform.position, Target, 4 * Time.deltaTime);
				if (transform.position == Target) { Stage += Forward; }
				break;
			case 3:
				Target = Target - transform.position;
				Target.x *= BoolToFloat(Order.x == 3);
				Target.y *= BoolToFloat(Order.y == 3);
				Target.z *= BoolToFloat(Order.z == 3);
				Target = Target + transform.position;

				transform.position = Vector3.MoveTowards(transform.position, Target, 4 * Time.deltaTime);
				if (transform.position == Target) { Stage += Forward;}
				break;
			case 4:
				Move = false;
				break;
		}
		return Stage;
	}
}
/*TargetDir = Target - this.transform.position;
float step = (float) 0.4 * Time.deltaTime;
Vector3 newDir = Vector3.RotateTowards(transform.forward, TargetDir, step, 0.0F);
Debug.DrawRay(transform.position, newDir, Color.red);
transform.rotation = Quaternion.LookRotation(newDir);*/
