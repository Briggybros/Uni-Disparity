using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class character : MonoBehaviour {

	private int direction = 0;
	private Vector3 originalPosition = new Vector3(0, 0, 0);
	private int move = 0;
	private int directionNormalised = 0;
	private int rotateflag = 0;
	private int movementflag = 0;
	private float RotationSpeed = 15.0f;

	void Rotate () {
		this.transform.rotation = Quaternion.Lerp (this.transform.rotation, Quaternion.Euler (0, direction * 90, 0), Time.deltaTime * RotationSpeed);
	}

	void Move (int translation) {
			if (translation == 1) {
			this.transform.position += transform.forward*Time.deltaTime;
			}
			if (translation == -1) {
			this.transform.position += transform.forward*-Time.deltaTime;
			}
	}

	void Start () {
		rotateflag = 0;
		movementflag = 0;
	}

	void Update () {

		if (rotateflag == 1) {
			Rotate ();

			directionNormalised = direction;
			if (directionNormalised < 0) {
				directionNormalised = directionNormalised + 4;
			}
			if ((directionNormalised * 90) - 1 < this.transform.rotation.eulerAngles.y && this.transform.rotation.eulerAngles.y < (directionNormalised * 90) + 1) {
				this.transform.eulerAngles = new Vector3 (0, directionNormalised * 90, 0);
				rotateflag = 0;
			}
		} 

		else if (movementflag == 1) {
			Move (move);
			Debug.Log (directionNormalised);
			if (directionNormalised == 0 || directionNormalised == 4) {
				if (move == 1) {
					if (originalPosition.z + 1 - 0.1 < this.transform.position.z && this.transform.position.z < originalPosition.z + 1 + 0.1) {
						movementflag = 0;
					}
				}
				if (move == -1) {
					if (originalPosition.z - 1 - 0.1 < this.transform.position.z && this.transform.position.z < originalPosition.z - 1 + 0.1) {
						movementflag = 0;
					}
				}
			}
			else if (directionNormalised == 1){
				if (move == 1) {
					if (originalPosition.x + 1 - 0.1 < this.transform.position.x && this.transform.position.x < originalPosition.x + 1 + 0.1) {
						movementflag = 0;
					}
				}
				if (move == -1) {
					if (originalPosition.x - 1 - 0.1 < this.transform.position.x && this.transform.position.x < originalPosition.x - 1 + 0.1) {
						movementflag = 0;
					}
				}
			}
			else if (directionNormalised == 2){
				if (move == 1) {
					if (originalPosition.z - 1 - 0.1 < this.transform.position.z && this.transform.position.z < originalPosition.z - 1 + 0.1) {
						movementflag = 0;
					}
				}
				if (move == -1) {
					if (originalPosition.z + 1 - 0.1 < this.transform.position.z && this.transform.position.z < originalPosition.z + 1 + 0.1) {
						movementflag = 0;
					}
				}
			}
			else if (directionNormalised == 3){
				if (move == 1) {
					if (originalPosition.x - 1 - 0.1 < this.transform.position.x && this.transform.position.x < originalPosition.x - 1 + 0.1) {
						movementflag = 0;
					}
				}
				if (move == -1) {
					if (originalPosition.x + 1 - 0.1 < this.transform.position.x && this.transform.position.x < originalPosition.x + 1 + 0.1) {
						movementflag = 0;
					}
				}
			}
		}
		else {
			if (Input.GetKeyDown (KeyCode.A)) {        // Left
				direction++;
				if (direction > 0) {
					direction = direction % 4;
				} 
				else {
					direction = direction % -4;
				}
				rotateflag = 1;
			}

			if (Input.GetKeyDown (KeyCode.D)) {        // Right
				direction--;
				if (direction > 0) {
					direction = direction % 4;
				} 
				else {
					direction = direction % -4;
				}
				rotateflag = 1;
			}

			if (Input.GetKeyDown (KeyCode.W)) {        // Up
				move = 1;
				movementflag = 1;
				originalPosition = this.transform.position;
			}

			if (Input.GetKeyDown (KeyCode.S)) {        // Down
				move = -1;
				movementflag = 1;
				originalPosition = this.transform.position;
			}
		}
	}
}
