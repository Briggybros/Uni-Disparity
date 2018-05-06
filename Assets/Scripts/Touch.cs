using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class Touch : EventTrigger {


    private RectTransform rt;
	private static List<string> buttonTouches = new List<string>();

	public static bool Test(string label) {
		foreach (string touchedLabel in buttonTouches) {
			if (touchedLabel == label) {
				return true;
			}
		}
		return false;
	}

    private void Start()
    {
        rt = gameObject.GetComponent<RectTransform>();
    }
    
	public override void OnPointerEnter(PointerEventData data) {
		buttonTouches.Add(this.name);
        rt.localScale = new Vector3(1.2f, 1.2f, 1.2f);
    }

	public override void OnPointerExit(PointerEventData data) {
		buttonTouches.Remove(this.name);
        rt.localScale = Vector3.one;
    }
}
