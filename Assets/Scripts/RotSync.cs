using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotSync : MonoBehaviour
{
  public GameObject parent, actualParent;
	
  void Update()
  {
    this.transform.SetParent(null);
    this.transform.rotation = parent.transform.rotation;
    Vector3 v = this.transform.rotation.eulerAngles;
    this.transform.rotation = Quaternion.Euler(-v.x + 10, v.y, v.z);
    this.transform.SetParent(actualParent.transform);
  }
}
