﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard_Script : MonoBehaviour {
  Transform cam;
	// Use this for initialization
	void Start () {
    cam = Camera.main.transform;
	}
	
	// Update is called once per frame
	void Update () {
    transform.LookAt(cam);
	}
}
