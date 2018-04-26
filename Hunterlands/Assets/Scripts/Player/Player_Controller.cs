using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Mouse0)){
      RaycastHit info;
      Physics.Raycast(Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f)), out info, 30f);
      if (info.transform != null){
        print(info.transform.name + ", " + info.collider.tag + " Hit!");
        Target_Debug_Script tarScr = info.transform.GetComponent<Target_Debug_Script>();
        if (tarScr != null) { 
          tarScr.TriggerHitInfo(10);
        }
      }
    }
	}
}
