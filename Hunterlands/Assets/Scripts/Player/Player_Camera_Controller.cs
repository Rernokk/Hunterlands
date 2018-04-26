using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Camera_Controller : MonoBehaviour {
  Transform parentObj;
	// Use this for initialization
	void Start () {
    //Fetching parent for rotational anchor on y-axis.
    parentObj = transform.parent;
	}
	
	// Update is called once per frame
	void Update () {
    //First Person Camera Rotation
    parentObj.Rotate(new Vector3(0, Time.deltaTime) * Input.GetAxis("Mouse X") * 30);
    transform.Rotate(new Vector3(Time.deltaTime, 0) * -Input.GetAxis("Mouse Y") * 30);
  }
}
