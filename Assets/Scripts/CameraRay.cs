using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRay : MonoBehaviour {

	public Camera camera;
	public LineRenderer line;
	bool laserActive;
	GameObject target;
	RaycastHit hit;

	// Use this for initialization
	void Start () {
		laserActive = false;
		line.enabled = true;
		target = GameObject.Find("keyDoor");
	}
	
	// Update is called once per frame
	void Update () {

	}

	void SwitchReceived () {
		Debug.Log("bamamam");
		if (laserActive == false) {
			Debug.Log("yassss");
			shootRay();
		}
		else if (laserActive == true) {
			stopShootRay();
		}
	}

	void shootRay () {
		//StopAllCoroutines();
		laserActive = true;
		StartCoroutine(fireRay());
	}

	void stopShootRay () {
		laserActive = false;
		line.enabled = true;
	}

	IEnumerator fireRay () {

		line.enabled = true;

	while (laserActive == true) {
			Debug.Log("here");
			Ray ray = new Ray(camera.transform.position, camera.transform.forward);
			line.SetPosition(0, ray.origin);

			/*if (Physics.Raycast (ray, out hit, 100, QueryTriggerInteraction = false)) {
				line.SetPosition(1, hit.point);
				if (hit.rigidbody.gameObject.tag == "keyDoor") {
					target.GetComponent<ListenerScript>().BroadcastMessage("SwitchFlag");
				} 
			}
			else { */
				line.SetPosition(1, ray.GetPoint(100));
			//}
		}
		yield return 0;
	}
}
