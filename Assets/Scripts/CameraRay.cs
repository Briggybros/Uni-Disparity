using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRay : Receiver {

	public new GameObject camera;
	public GameObject target;
	public LineRenderer line;
	bool laserActive;
	//public GameObject target;
	RaycastHit hit;

	Vector3 prevForward;

	// Use this for initialization
	protected override void Start () {
		laserActive = false;
		line.enabled = false;
	}

	protected override void SwitchReceived () {
		 if (laserActive == false) {
		 	shootRay();
		 }
		 else if (laserActive == true) {
		 	stopShootRay();
		 }
	}

	void shootRay () {
		// StopAllCoroutines();
		laserActive = true;
		StartCoroutine(fireRay());
	}

	void stopShootRay () {
		laserActive = false;
		line.enabled = false;
	}

	IEnumerator fireRay () {

		while (laserActive == true) {
			line.enabled = true;
			if(camera.transform.forward != prevForward) {
				prevForward = camera.transform.forward;
				Ray ray = new Ray(camera.transform.position, camera.transform.forward);
				line.SetPosition(0, ray.origin);

				if (Physics.Raycast (ray, out hit, 100)) {
					Debug.Log(hit.transform.gameObject.name);
					line.SetPosition(1, hit.point);
					if (hit.rigidbody != null && hit.rigidbody.gameObject.tag == "KeyDoor") {
						hit.rigidbody.gameObject.GetComponent<ListenerScript>().BroadcastMessage("SwitchFlag");
					} 
				}
				else { 
					line.SetPosition(1, ray.GetPoint(100));
				} 
			}
			yield return 0;
		}
	}	
}
