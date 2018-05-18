using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class EnemyHUDManager : NetworkBehaviour {
  Enemy enemyData;
  CanvasGroup hpBar;

  [SerializeField]
  GameObject HUDPrefab;

  [SerializeField]
  Gradient healthGradient;

  private void Start()
  {
    enemyData = GetComponent<Enemy>();

    //Creating Heatlh Bar & HUD Display
    HUDPrefab = Instantiate(HUDPrefab, transform, false);
    HUDPrefab.name = "EnemyHUD";
    hpBar = HUDPrefab.GetComponent<CanvasGroup>();
  }
  
  public void UpdateHealthBar(float val)
  {
    //transform.Find("EnemyHUD/Healthbar/Health/Bar").GetComponent<Image>().color = healthGradient.Evaluate(enemyData.HealthPercent);
    //transform.Find("EnemyHUD/Healthbar").GetComponent<Slider>().value = enemyData.HealthPercent;
    if (enemyData.inCombat)
    {
      if (hpBar.alpha == 0){
        hpBar.alpha = 1;
      }
      transform.Find("EnemyHUD/Healthbar/Health/Bar").GetComponent<Image>().color = healthGradient.Evaluate(val);
      transform.Find("EnemyHUD/Healthbar").GetComponent<Slider>().value = val;
    } else {
      if (hpBar.alpha == 1){
        hpBar.alpha = 0;
      }
    }
  }
  
}
