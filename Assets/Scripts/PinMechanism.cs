using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PinMechanism : Receiver {
    
    bool OverlayOn;
    bool success;
    private Vector3 spikeTarget;
    public float DownSpeed, MovementRange;
    public GameObject PinEntryUI, MovementUI;
    public GameObject[] Slot1, Slot2, Slot3, Slot4;
    public GameObject Failed;
    public GameObject target, target1, target2;
    public GameObject Spikes;
    public Button up1, up2, up3, up4, down1, down2, down3, down4, Back, Unlock;
    public int[] CurrentPin;
    private int[] pin = {1,2,3,4};

	// Use this for initialization
	protected override void Start () {
        success = false;
        spikeTarget = Spikes.transform.position;
        spikeTarget.y += MovementRange;
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
	}

    protected override void Update() {
        if (Spikes.transform.position == spikeTarget) {
            target.SetActive(false);
        }
    }

    int mod(int x, int m) {
        return (x%m + m)%m;
    }

    void TaskOnClickUp (int num, GameObject[] slotNum) {
        //check buttons are pressed
        slotNum[CurrentPin[num-1]].SetActive(false);
        slotNum[mod(CurrentPin[num-1] + 1, 10)].SetActive(true);
        CurrentPin[num-1] = mod(CurrentPin[num-1] + 1, 10);
    }

    void TaskOnClickDown (int num, GameObject[] slotNum) {
        //check buttons are pressed
        slotNum[CurrentPin[num-1]].SetActive(false);
        slotNum[mod(CurrentPin[num-1] - 1, 10)].SetActive(true);
        CurrentPin[num-1] = mod(CurrentPin[num-1] - 1, 10);
    }

    void Verify () {
        for (int i = 0; i < 4; i++) {
            if (CurrentPin[i] != pin[i]) {
                Failed.SetActive(true);
                return;
            }
        }
        // preferably have a textbox pop up saying successfully unlocked
        DisablePinEntry();
        StartCoroutine(LowerSpikes());
        target1.SetActive(false);
        target2.SetActive(false);
    }

    protected override void SwitchReceived()
    {
        if (OverlayOn == false)
        {
            EnablePinEntry();
        }
    }

    IEnumerator LowerSpikes () {
        while (Spikes.transform.position.y > spikeTarget.y) {
            Spikes.transform.position = Vector3.MoveTowards(Spikes.transform.position, spikeTarget, DownSpeed * Time.deltaTime);
            yield return 0;
        }
    }

    public void EnablePinEntry () {
        OverlayOn = true;
        PinEntryUI.SetActive(true);
        MovementUI.SetActive(false);
        Failed.SetActive(false);
        for (int j = 0; j < 4; j++) {
            CurrentPin[j] = 0;
        }
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
        OverlayOn = false;
        PinEntryUI.SetActive(false);
        MovementUI.SetActive(true);
    }
}