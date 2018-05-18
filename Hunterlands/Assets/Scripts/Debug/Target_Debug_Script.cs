using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Target_Debug_Script : MonoBehaviour {
  [SerializeField]
  GameObject myCanvas;

  private float counter = 0;

	// Use this for initialization
	void Start () {
    myCanvas = Instantiate(myCanvas, transform, false);
    myCanvas.transform.position += Vector3.up * 1.5f;
	}
	
	// Update is called once per frame
	void Update () {
		if (counter > 0){
      counter -= Time.deltaTime;
    } else if (counter < 0){
      counter = 0;
      myCanvas.transform.Find("Text").GetComponent<Text>().text = "";
    }
	}

  public void TriggerHitInfo(int damage){
    print("Object hit!");
    myCanvas.transform.Find("Text").GetComponent<Text>().text = damage.ToString();
    counter = 1.0f;
  }
  
}
