using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PinMechanism : Receiver
{
  public float DownSpeed, MovementRange;
  public GameObject PinEntryUI, MovementUI, Slot1, Slot2, Slot3, Slot4, Failed, target, target1, target2, Spikes;
  public Button up1, up2, up3, up4, down1, down2, down3, down4, Back, Unlock;
  public int[] CurrentPin;

  private bool OverlayOn;
  private Vector3 spikeTarget;
  private int[] pin = { 5, 7, 3, 2 };

  protected override void Start()
  {
    spikeTarget = Spikes.transform.position;
    spikeTarget.y += MovementRange;
    OverlayOn = false;
    CurrentPin = new int[4];
    up1.onClick.AddListener(delegate { TaskOnClickUp(0, Slot1); });
    up2.onClick.AddListener(delegate { TaskOnClickUp(1, Slot2); });
    up3.onClick.AddListener(delegate { TaskOnClickUp(2, Slot3); });
    up4.onClick.AddListener(delegate { TaskOnClickUp(3, Slot4); });
    down1.onClick.AddListener(delegate { TaskOnClickDown(0, Slot1); });
    down2.onClick.AddListener(delegate { TaskOnClickDown(1, Slot2); });
    down3.onClick.AddListener(delegate { TaskOnClickDown(2, Slot3); });
    down4.onClick.AddListener(delegate { TaskOnClickDown(3, Slot4); });
    Back.onClick.AddListener(DisablePinEntry);
    Unlock.onClick.AddListener(Verify);
  }

  protected override void Update()
  {
    if (Spikes.transform.position == spikeTarget)
    {
      target.SetActive(false);
    }
  }

  int mod(int x, int m)
  {
    return (x % m + m) % m;
  }

  void TaskOnClickUp(int num, GameObject slotNum)
  {
    CurrentPin[num] = mod(CurrentPin[num] + 1, 10);
    slotNum.transform.GetChild(0).GetComponent<Text>().text = CurrentPin[num].ToString();
  }

  void TaskOnClickDown(int num, GameObject slotNum)
  {
    CurrentPin[num] = mod(CurrentPin[num] - 1, 10);
    slotNum.transform.GetChild(0).GetComponent<Text>().text = CurrentPin[num].ToString();
  }

  void Verify()
  {
    for (int i = 0; i < 4; i++)
    {
      if (CurrentPin[i] != pin[i])
      {
        Failed.SetActive(true);
        return;
      }
    }
    DisablePinEntry();
    StartCoroutine(LowerSpikes());
    target1.SetActive(false);
    target2.SetActive(false);
  }

  protected override void SwitchReceived()
  {
    if ((!OverlayOn) && (CharacterPicker.GetWorld() == CharacterPicker.WORLDS.DOG))
    {
      EnablePinEntry();
    }
  }

  IEnumerator LowerSpikes()
  {
    while (Spikes.transform.position.y > spikeTarget.y)
    {
      Spikes.transform.position = Vector3.MoveTowards(Spikes.transform.position, spikeTarget, DownSpeed * Time.deltaTime);
      yield return 0;
    }
  }

  public void EnablePinEntry()
  {
    OverlayOn = true;
    PinEntryUI.SetActive(true);
    Failed.SetActive(false);
  }

  public void DisablePinEntry()
  {
    OverlayOn = false;
    PinEntryUI.SetActive(false);
    MovementUI.SetActive(true);
  }
}
