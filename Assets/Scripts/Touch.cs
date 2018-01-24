using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Touch : MonoBehaviour {
	private static List<string> buttonTouches = new List<string>();

	public static bool Test(string label) {
		foreach (string touchedLabel in buttonTouches) {
			if (touchedLabel == label) {
				return true;
			}
		}
		return false;
	}

	public string label;

	public void OnPointerEnter() {
		buttonTouches.Add(label);
	}

	public void OnPointerExit() {
		buttonTouches.Remove(label);
	}
}
