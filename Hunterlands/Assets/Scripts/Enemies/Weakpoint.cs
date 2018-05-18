using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weakpoint : MonoBehaviour {
  [SerializeField]
  WeaponType[] criticalWeapons;
  Enemy enemyScript;

  [SerializeField]
  float critMultiplier;

  public float CritMultiplier{
    get{
      return critMultiplier;
    }

    set {
      critMultiplier = value;
    }
  }

	void Start () {
    GetComponent<MeshRenderer>().material.color = Color.red;
    enemyScript = GetComponentInParent<Enemy>();
	}

  public bool CompareWeaponType(WeaponType type){
    for (int i = 0; i < criticalWeapons.Length; i++){
      if (criticalWeapons[i] == type){
        return true;
      }
    }
    return false;
  }
}
