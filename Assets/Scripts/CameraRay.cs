using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CameraRay : Receiver {

	public new GameObject camera;
	public LineRenderer line;
	public ParticleSystem laserParticles;
	bool laserActive;
	RaycastHit hit;
	private Light laserLight;

	Vector3 prevForward;

	public GameObject[] players;
	public GameObject heldPlayer;

	// Use this for initialization
	protected override void Start() {
		laserActive = false;
		line.enabled = false;
		laserLight = camera.GetComponent<Light>();
		laserLight.enabled = false;

		players = GameObject.FindGameObjectsWithTag("Player");
		foreach (GameObject player in players) {
			if (player.GetComponent<JoystickCharacter>().isLocalPlayer) {
				heldPlayer = player;
			}
		}

	}

	protected override void SwitchReceived() {
		if (laserActive == false) {
			shootRay();
		} else if (laserActive == true) {
			stopShootRay();
		}
	}

	protected override void Update() {
		if (heldPlayer == null) {
			players = GameObject.FindGameObjectsWithTag("Player");
			foreach (GameObject player in players) {
				if (player.GetComponent<JoystickCharacter>().isLocalPlayer) {
					heldPlayer = player;
				}
			}
		}
	}


	void shootRay() {
		laserActive = true;
		StartCoroutine(fireRay());
	}

	void stopShootRay() {
		laserActive = false;
		line.enabled = false;
		laserLight.enabled = false;
		laserParticles.Stop();
	}

	IEnumerator fireRay() {

		while (laserActive == true) {
			laserLight.enabled = true;
			line.enabled = true;
			laserParticles.Play();
			if (camera.transform.forward != prevForward) {
				prevForward = camera.transform.forward;
				Ray ray = new Ray(camera.transform.position, camera.transform.forward);
				line.SetPosition(0, ray.origin);

				if (Physics.Raycast(ray, out hit, 100)) {
					line.SetPosition(1, hit.point);
					laserParticles.transform.position = hit.point;
					laserParticles.transform.rotation = Quaternion.LookRotation(ray.origin - hit.point);

					if (hit.rigidbody != null && hit.rigidbody.gameObject.tag == "KeyDoor") {
						heldPlayer.BroadcastMessage("TargetSwitch", hit.rigidbody.gameObject);
					}
				} else {
					line.SetPosition(1, ray.GetPoint(100));
				}
			}
			yield return 0;
		}
	}
}