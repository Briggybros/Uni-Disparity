using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    private bool uiOpen = false;
    private float matchWidth = 1;
    public GameObject uiControls;


    public bool isOpen()
    {
        return uiOpen;
    }

    void setOpen(bool open)
    {
        uiOpen = open;
    }

	// Use this for initialization
	void Start () {
		if (Screen.width < Screen.height) matchWidth = 0;
        initialiseUI();
	}

    GameObject createCanvas(string tag)
    {
        GameObject parent = new GameObject(tag);
        parent.AddComponent<Canvas>();
        parent.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
        parent.AddComponent<CanvasScaler>();
        parent.GetComponent<CanvasScaler>().uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        parent.GetComponent<CanvasScaler>().referenceResolution = new Vector2(1080, 720);
        parent.AddComponent<GraphicRaycaster>();
        parent.layer = 5;
        parent.GetComponent<CanvasScaler>().matchWidthOrHeight = matchWidth;

        return parent;
    }

    void initialiseUI()
    {
        GameObject daddy = createCanvas("UI");
        GameObject instantiated = Instantiate(uiControls, new Vector2(0, 0), Quaternion.identity) as GameObject;
        instantiated.transform.SetParent(daddy.transform, false);
    }
}
