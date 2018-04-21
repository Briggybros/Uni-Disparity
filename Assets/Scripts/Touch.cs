using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class Touch : EventTrigger {


    private RectTransform rt;
    private bool running = false;
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
        gameObject.GetComponent<Button>().onClick.AddListener(GrowCaller);
    }

    void GrowCaller()
    {
        if(!running) StartCoroutine(Grow());
    }

    //gives button a growth effect
    private IEnumerator Grow()
    {
        running = true;
        bool grown = false;
        while (true)
        {
            if (!grown)
            {
                rt.localScale = new Vector3(1.2f, 1.2f, 1.2f);
                grown = true;
                yield return new WaitForSeconds(0.1f);
            }
            else
            {
                rt.localScale = Vector3.one;
                running = false;
                yield break;
            }
        }
        
    }

	public override void OnPointerEnter(PointerEventData data) {
		buttonTouches.Add(this.name);
	}

	public override void OnPointerExit(PointerEventData data) {
		buttonTouches.Remove(this.name);
	}
}
