using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public enum CameraMode { FIRST, THIRD };
public class Player_Camera_Controller : NetworkBehaviour {
  [SerializeField]
  CameraMode camMode = CameraMode.FIRST;

  [SerializeField]
  GameObject cameraSet;
  Transform currentCam;
  

  public Transform CurrentCamera{
    get {
      return currentCam;
    }

    set{
      currentCam = value;
    }
  }

  public CameraMode CamMode{
    get{
      return camMode;
    }

    set {
      camMode = value;
    }
  }

	void Start ()
  {
    if (!isLocalPlayer)
    {
      return;
    }
    Instantiate(cameraSet, transform, false);
    transform.Find("CamAnchors(Clone)").name = "CamAnchors";
    currentCam = transform.Find("CamAnchors/TPCamAnchor/Camera");
    //Cursor.lockState = CursorLockMode.Locked;
    //Cursor.visible = false;
  }
	
	void Update () {
    if (!isLocalPlayer){
      return;
    }
    if (Input.GetKeyDown(KeyCode.Escape)){
      Cursor.visible = true;
      Cursor.lockState = CursorLockMode.None;
    }
    if (!Cursor.visible || true)
    {
      if (camMode == CameraMode.FIRST)
      {
        if (Input.GetKeyDown(KeyCode.KeypadPlus))
        {
          camMode = CameraMode.THIRD;
          transform.Find("CamAnchors/FPCamAnchor/Camera").gameObject.SetActive(false);
          transform.Find("CamAnchors/TPCamAnchor/Camera").gameObject.SetActive(true);
          currentCam = transform.Find("CamAnchors/TPCamAnchor/Camera");
        }
        transform.Find("CamAnchors/FPCamAnchor").transform.Rotate(new Vector3(0, Time.deltaTime) * Input.GetAxis("Mouse X") * 30);
        transform.Find("CamAnchors/FPCamAnchor/Camera").transform.Rotate(new Vector3(Time.deltaTime, 0) * -Input.GetAxis("Mouse Y") * 30);
      }
      else if (camMode == CameraMode.THIRD)
      {
        if (Input.GetKeyDown(KeyCode.KeypadPlus))
        {
          camMode = CameraMode.FIRST;
          transform.Find("CamAnchors/FPCamAnchor/Camera").gameObject.SetActive(true);
          transform.Find("CamAnchors/TPCamAnchor/Camera").gameObject.SetActive(false);
          currentCam = transform.Find("CamAnchors/FPCamAnchor/Camera");
        }
      }
    }
  }
}
