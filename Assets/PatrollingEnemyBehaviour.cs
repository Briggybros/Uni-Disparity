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
		TempStage = 1;
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
			/*Debug.Log(TempStage + " = " + Stage);
			if(TempStage != Stage && Stage != 0 && Stage != 4){
				Target = Target - transform.position;
				Target.x *= BoolToFloat(Order.x == Stage);
				Target.y *= BoolToFloat(Order.y == Stage);
				Target.z *= BoolToFloat(Order.z == Stage);
				Target = Target + transform.position;
				TargetDir = Target - this.transform.position;
				float step = (float) 0.4 * Time.deltaTime;
				Vector3 newDir = Vector3.RotateTowards(transform.forward, TargetDir, step, 0.0F);
				Debug.DrawRay(transform.position, newDir, Color.red);
				transform.rotation = Quaternion.LookRotation(newDir);
				if(Vector3.Angle( transform.forward, newDir) < 1){
					TempStage = Stage;
				}
			}else{*/
				Stage = Movement(Stage,TempTarget,Order,Forward);
			//}
		}else{
			Debug.Log("fetch next Move");
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
				Debug.Log("Finished Move");
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
				Debug.Log("Finished Move");
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
