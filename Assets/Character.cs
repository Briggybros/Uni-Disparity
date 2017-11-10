using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

	private int Direction = 0;
	private Vector3 OriginalPosition = new Vector3(0, 0, 0);
	private int MoveInt = 0;
	private int DirectionNormalised = 0;
	private int Rotateflag = 0;
	private int Movementflag = 0;
	private float RotationSpeed = 15.0f;
	private float MovementSpeed = 4.0f;

	void Rotate () {
		this.transform.localRotation = Quaternion.Lerp (
			this.transform.localRotation,
			Quaternion.Euler (
				0, Direction * 90, 0
			),
			Time.deltaTime * RotationSpeed
		);
	}

	void Move (int translation) {
			if (translation == 1) {
				this.transform.position +=
				transform.forward*Time.deltaTime*MovementSpeed;
			}
			if (translation == -1) {
				this.transform.position +=
				transform.forward*-Time.deltaTime*MovementSpeed;
			}
	}

	void Start () {
		Rotateflag = 0;
		Movementflag = 0;
	}

	void Update () {

		if (Rotateflag == 1) {
			Rotate ();

			DirectionNormalised = Direction;
				if (DirectionNormalised < 0) {
					DirectionNormalised = DirectionNormalised + 4;
			}
				if ((DirectionNormalised * 90) - 1 <
				this.transform.localRotation.eulerAngles.y &&
				this.transform.localRotation.eulerAngles.y <
				(DirectionNormalised * 90) + 1) {
					this.transform.eulerAngles = new Vector3 (
					0, DirectionNormalised * 90, 0
				);
				Rotateflag = 0;
			}
		}

			else if (Movementflag == 1) {

			Move (MoveInt);
			Debug.Log (DirectionNormalised);
			switch (DirectionNormalised) {

			case 0:
				if (MoveInt == 1) {
					if (OriginalPosition.z + 1 - 0.1 <
					this.transform.position.z &&
					this.transform.position.z < OriginalPosition.z + 1 + 0.1) {
						this.transform.position = new Vector3 (
							OriginalPosition.x, OriginalPosition.y, OriginalPosition.z + 1
						);
						Movementflag = 0;
					}
				}
				if (MoveInt == -1) {
					if (OriginalPosition.z - 1 - 0.1 <
					this.transform.position.z &&
					this.transform.position.z < OriginalPosition.z - 1 + 0.1) {
						this.transform.position = new Vector3 (
							OriginalPosition.x, OriginalPosition.y, OriginalPosition.z - 1
						);
						Movementflag = 0;
					}
				}
				break;

			case 1:
				if (MoveInt == 1) {
					if (OriginalPosition.x + 1 - 0.1 <
					this.transform.position.x &&
					this.transform.position.x < OriginalPosition.x + 1 + 0.1) {
						this.transform.position = new Vector3 (
							OriginalPosition.x + 1, OriginalPosition.y, OriginalPosition.z
						);
						Movementflag = 0;
					}
				}
				if (MoveInt == -1) {
					if (OriginalPosition.x - 1 - 0.1 <
					this.transform.position.x &&
					this.transform.position.x < OriginalPosition.x - 1 + 0.1) {
						this.transform.position = new Vector3 (
						OriginalPosition.x - 1, OriginalPosition.y, OriginalPosition.z
					);
						Movementflag = 0;
					}
				}
				break;

			case 2:
				if (MoveInt == 1) {
					if (OriginalPosition.z - 1 - 0.1 <
					this.transform.position.z &&
					this.transform.position.z < OriginalPosition.z - 1 + 0.1) {
						this.transform.position = new Vector3 (
						OriginalPosition.x, OriginalPosition.y, OriginalPosition.z - 1
					);
						Movementflag = 0;
					}
				}
				if (MoveInt == -1) {
					if (OriginalPosition.z + 1 - 0.1 <
					this.transform.position.z &&
					this.transform.position.z < OriginalPosition.z + 1 + 0.1) {
						this.transform.position = new Vector3 (
						OriginalPosition.x, OriginalPosition.y, OriginalPosition.z + 1
					);
						Movementflag = 0;
					}
				}
				break;

			case 3:
				if (MoveInt == 1) {
					if (OriginalPosition.x - 1 - 0.1 <
					this.transform.position.x &&
					this.transform.position.x < OriginalPosition.x - 1 + 0.1) {
						this.transform.position = new Vector3 (
						OriginalPosition.x - 1, OriginalPosition.y, OriginalPosition.z
					);
						Movementflag = 0;
					}
				}
				if (MoveInt == -1) {
					if (OriginalPosition.x + 1 - 0.1 <
					this.transform.position.x &&
					this.transform.position.x < OriginalPosition.x + 1 + 0.1) {
						this.transform.position = new Vector3 (
						OriginalPosition.x + 1, OriginalPosition.y, OriginalPosition.z
					);
						Movementflag = 0;
					}
				}
				break;
			}
		}

		else {
			if (Input.GetKeyDown (KeyCode.D)) {        // Left
				Direction++;
				if (Direction > 0) {
					Direction = Direction % 4;
				}
				else {
					Direction = Direction % -4;
				}
				Rotateflag = 1;
			}

			if (Input.GetKeyDown (KeyCode.A)) {        // Right
				Direction--;
				if (Direction > 0) {
					Direction = Direction % 4;
				}
				else {
					Direction = Direction % -4;
				}
				Rotateflag = 1;
			}

			if (Input.GetKeyDown (KeyCode.W)) {        // Up
				MoveInt = 1;
				Movementflag = 1;
				OriginalPosition = this.transform.position;
			}

			if (Input.GetKeyDown (KeyCode.S)) {        // Down
				MoveInt = -1;
				Movementflag = 1;
				OriginalPosition = this.transform.position;
			}
		}
	}
}
