using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARStunEnemy : Enemy {
  protected override void WeakpointAction(string playerID)
  {
    print("Sniper Weakpoint Hit, Knockback.");
    Vector3 forceDir = (transform.position - GameObject.Find(playerID).transform.position);
    forceDir.y = 0;
    forceDir.Normalize();
    rgd.AddForce(forceDir * 20, ForceMode.Impulse);
  }
}
