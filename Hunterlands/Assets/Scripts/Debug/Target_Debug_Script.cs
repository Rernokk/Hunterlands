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
    myCanvas = Instantiate(myCanvas, new Vector3(transform.position.x, transform.position.y + 3, transform.position.z), Quaternion.identity);
    myCanvas.transform.parent = transform;
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
    counter = 3.0f;
  }
}
