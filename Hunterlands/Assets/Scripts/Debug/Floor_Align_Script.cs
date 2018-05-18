using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor_Align_Script : MonoBehaviour
{

  // Use this for initialization
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {
    RaycastHit info;
    Physics.Raycast(transform.position, -transform.up, out info);
    if (info.transform != null)
    {
      transform.up = info.normal;
      transform.position = info.point + info.normal;
    }

    transform.Rotate(0, Time.deltaTime * 180, 0);
    transform.position += transform.forward * Time.deltaTime;

  }
}
