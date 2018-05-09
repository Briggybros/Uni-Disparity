using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class forcePos : MonoBehaviour
{
  public float x, y, z;

  void Start()
  {
    transform.localPosition = new Vector3(x, y, z);
  }
}
