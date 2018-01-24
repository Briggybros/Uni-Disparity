using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Touch : EventTrigger {
	private static List<string> buttonTouches = new List<string>();

	public static bool Test(string label) {
		foreach (string touchedLabel in buttonTouches) {
			if (touchedLabel == label) {
				return true;
			}
		}
		return false;
	}

	public override void OnPointerEnter(PointerEventData data) {
		Debug.Log(this.name + " entered");
		buttonTouches.Add(this.name);
	}

	public override void OnPointerExit(PointerEventData data) {
		Debug.Log(this.name + " exited");
		buttonTouches.Remove(this.name);
	}
}
