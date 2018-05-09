using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
  public GameObject uiCanvas;

  public bool IsOpen()
  {
    return uiCanvas.activeInHierarchy;
  }

  public void SetOpen(bool open)
  {
    uiCanvas.SetActive(open);
  }
}
