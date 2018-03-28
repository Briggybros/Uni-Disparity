using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PinMechanism : Receiver {
    
    bool StopPlayerMovement;
    bool OverlayOn;
    public GameObject PinEntryUI;
    public GameObject MovementUI;
    public GameObject[] Slot1;
    public GameObject[] Slot2;
    public GameObject[] Slot3;
    public GameObject[] Slot4;
    public GameObject Failed;
    public GameObject target;
    public Button up1;
    public Button up2;
    public Button up3;
    public Button up4;
    public Button down1;
    public Button down2;
    public Button down3;
    public Button down4;
    public Button Back;
    public Button Unlock;
    public int[] CurrentPin;
    private int[] pin = {1,2,3,4};

	// Use this for initialization
	protected override void Start () {
        DisablePinEntry();
        CurrentPin = new int[4];
        up1.onClick.AddListener(delegate{TaskOnClickUp(1, Slot1);});
        up2.onClick.AddListener(delegate{TaskOnClickUp(2, Slot2);});
        up3.onClick.AddListener(delegate{TaskOnClickUp(3, Slot3);});
        up4.onClick.AddListener(delegate{TaskOnClickUp(4, Slot4);});
        down1.onClick.AddListener(delegate{TaskOnClickDown(1, Slot1);});
        down2.onClick.AddListener(delegate{TaskOnClickDown(2, Slot2);});
        down3.onClick.AddListener(delegate{TaskOnClickDown(3, Slot3);});
        down4.onClick.AddListener(delegate{TaskOnClickDown(4, Slot4);});
        Back.onClick.AddListener(DisablePinEntry);
        Unlock.onClick.AddListener(Verify);
        //populate slots
        // Slot1 = new GameObject[10];
        // Slot2 = new GameObject[10];
        // Slot3 = new GameObject[10];
        // Slot4 = new GameObject[10];

        // for (int i = 0; i < 10; i++) {
        //     Debug.Log("setting images to slots");
        //     string a = "Slot1" + i;
        //     string b = "Slot2" + i;
        //     string c = "Slot3" + i;
        //     string d = "Slot4" + i;
        //     // Debug.Log(GameObject.Find(a).name);
        //     Slot1[i] = GameObject.Find(a);
        //     Slot2[i] = GameObject.Find(b);
        //     Slot3[i] = GameObject.Find(c);
        //     Slot4[i] = GameObject.Find(d);
        // }
	}

    int mod(int x, int m) {
    return (x%m + m)%m;
}
    void TaskOnClickUp (int num, GameObject[] slotNum) {
        //check buttons are pressed
        Debug.Log("upclicked");
        slotNum[CurrentPin[num-1]].SetActive(false);
        slotNum[mod(CurrentPin[num-1] + 1, 10)].SetActive(true);
        CurrentPin[num-1] = mod(CurrentPin[num-1] + 1, 10);
        Debug.Log(CurrentPin[3]);
    }

    void TaskOnClickDown (int num, GameObject[] slotNum) {
        //check buttons are pressed
        // Debug.Log((CurrentPin[num-1] - 1)%10);
        slotNum[CurrentPin[num-1]].SetActive(false);
        slotNum[mod(CurrentPin[num-1] - 1, 10)].SetActive(true);
        CurrentPin[num-1] = mod(CurrentPin[num-1] - 1, 10);
        Debug.Log(CurrentPin[0]);
    }

    void Verify () {
        for (int i = 0; i < 4; i++) {
            if (CurrentPin[i] != pin[i]) {
                Failed.SetActive(true);
                return;
            }
        }
        Debug.Log("success");
        // preferably have a textbox pop up saying successfully unlocked
        DisablePinEntry();
        target.SetActive(false);
    }

    protected override void SwitchReceived()
    {
        Debug.Log("switch triggered");
        if (OverlayOn == false)
        {
            Debug.Log("here");
            EnablePinEntry();
        }
    }

    public void EnablePinEntry () {
        OverlayOn = true;
        PinEntryUI.SetActive(true);
        MovementUI.SetActive(false);
        Failed.SetActive(false);
        Debug.Log("disabled movement UI");
        for (int j = 0; j < 4; j++) {
            CurrentPin[j] = 0;
        }
        Debug.Log("set current pin to 0000");
        // set numbers to 0000
        Slot1[0].SetActive(true);
        Slot2[0].SetActive(true);
        Slot3[0].SetActive(true);
        Slot4[0].SetActive(true);
        for (int i = 1; i < 10; i++) {
            Slot1[i].SetActive(false);
            Slot2[i].SetActive(false);
            Slot3[i].SetActive(false);
            Slot4[i].SetActive(false);
        }
    }

    public void DisablePinEntry () {
        Debug.Log("disabling");
        OverlayOn = false;
        PinEntryUI.SetActive(false);
        MovementUI.SetActive(true);
        Debug.Log("disabled");
    }
}