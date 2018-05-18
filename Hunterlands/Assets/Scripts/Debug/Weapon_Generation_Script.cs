using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class Weapon_Generation_Script : MonoBehaviour
{
  [SerializeField]
  Weapon_Class currentWeapon;
  Player_Controller ctrller;

  public Weapon_Class CurrentWeapon{
    get {
      return currentWeapon;
    }

    set {
      currentWeapon = value;
      ctrller.CurrentWeapon = value;
    }
  }

  private void Start()
  {
    ctrller = GetComponent<Player_Controller>();
    CurrentWeapon = null;
  }

  // Update is called once per frame
  void Update()
  {
    if (Input.GetKeyDown(KeyCode.G))
    {
      CurrentWeapon = GenerateWeapon();
    }
  }

  Weapon_Class GenerateWeapon()
  {
    Weapon_Class generatedWeapon = null;
    switch (Random.Range(0, 2))
    {
      case (0):
        generatedWeapon = new SMG();
        break;
      default:
        generatedWeapon = new SniperRifle();
        break;
    }
    return generatedWeapon;
  }
}
