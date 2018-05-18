using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Billboard_Script : NetworkBehaviour
{
  public Transform cam;
  public Player_Camera_Controller tarCtrl;
  // Use this for initialization
  void Start()
  {
    cam = null;
    foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Player"))
    {
      if (obj.GetComponent<NetworkIdentity>().isLocalPlayer)
      {
        tarCtrl = obj.GetComponent<Player_Camera_Controller>();
        cam = tarCtrl.CurrentCamera;
        break;
      }
    }
  }

  // Update is called once per frame
  void Update()
  {
    if (tarCtrl != null)
    {
      cam = tarCtrl.CurrentCamera;
      transform.LookAt(cam);
    }
    else {
      foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Player"))
      {
        if (obj.GetComponent<NetworkIdentity>().isLocalPlayer)
        {
          tarCtrl = obj.GetComponent<Player_Camera_Controller>();
          cam = tarCtrl.CurrentCamera;
          break;
        }
      }
    }
  }
}
