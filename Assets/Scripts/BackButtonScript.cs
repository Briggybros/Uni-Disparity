using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackButtonScript : MonoBehaviour
{
  public void OnGUI()
  {
    if (Event.current.Equals(Event.KeyboardEvent("escape")))
    {
      GetComponent<Button>().onClick.Invoke();
    }
  }
}
