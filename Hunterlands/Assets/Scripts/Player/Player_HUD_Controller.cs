using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_HUD_Controller : MonoBehaviour
{
  public Player_Controller ctrller;
  // Use this for initialization
  void Start()
  {
    ctrller = transform.parent.parent.GetComponent<Player_Controller>();
  }

  // Update is called once per frame
  void Update()
  {
    if (ctrller.CurrentWeapon != null)
    {
      transform.Find("CurrentWeapon/Text").GetComponent<Text>().text = ctrller.CurrentWeapon.weaponName;
    }
    //transform.Find("CurrentWeapon/Text").GetComponent<Text>().text = "";
  }
}
