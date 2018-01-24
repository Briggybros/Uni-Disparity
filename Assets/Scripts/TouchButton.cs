using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchButton : MonoBehaviour {

	public string label;
	void Update () {
		for (int i = 0; i < Input.touchCount; i++) {
			if (EventSystem.current.IsPointerOverGameObject(i)) {
				if (Input.GetTouch(i).phase == TouchPhase.Began) {
					TouchManager.Update(label, true);
				} else if (Input.GetTouch(i).phase == TouchPhase.Ended) {
					TouchManager.Update(label, false);
				}
			}
		}
	}
}
