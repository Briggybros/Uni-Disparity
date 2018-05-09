using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AltMaterial : MonoBehaviour
{
  public Material altMaterial;

  void Start()
  {
    if (CharacterPicker.GetWorld() == CharacterPicker.WORLDS.DOG)
    {
      GetComponent<Renderer>().sharedMaterial = altMaterial;
    }
  }
}
