using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PinMechanism : Receiver {
    
    bool StopPlayerMovement;
    bool OverlayOn;
    public GameObject PinEntryUI;

	// Use this for initialization
	void Start () {
        DisablePinEntry();
	}

    protected override void SwitchReceived()
    {
        Debug.Log("switch triggered");
        if (OverlayOn == false)
        {
            Debug.Log("here");
            EnablePinEntry();
        }
        else if (OverlayOn == true)
        {
            DisablePinEntry();
        }
    }

    public void EnablePinEntry () {
        OverlayOn = true;
        PinEntryUI.SetActive(true);
    }

    public void DisablePinEntry () {
        OverlayOn = false;
        PinEntryUI.SetActive(false);
    }
}