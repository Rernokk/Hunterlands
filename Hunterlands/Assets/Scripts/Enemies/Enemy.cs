using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(EnemyHUDManager))]
[RequireComponent(typeof(Rigidbody))]
public class Enemy : NetworkBehaviour
{
  public static Dictionary<string, Enemy> EnemyDictionary;
  Weakpoint[] weakPoints;
  
  public static Enemy GetEnemy(string id)
  {
    return EnemyDictionary[id];
  }

  [SyncVar(hook = "RpcTriggerHealthUpdate")]
  float currentHealth;

  [SerializeField]
  float maxHealth;

  EnemyHUDManager hudManager;
  protected Rigidbody rgd;
  public bool inCombat;

  public float CurrentHealth
  {
    get
    {
      return currentHealth;
    }

    set
    {
      currentHealth = value;
    }
  }
  public float HealthPercent
  {
    get
    {
      return (currentHealth) / (maxHealth);
    }
  }
  protected void Start()
  {
    hudManager = GetComponent<EnemyHUDManager>();
    CurrentHealth = maxHealth;
    transform.name += "_" + GetComponent<NetworkIdentity>().netId;
    weakPoints = transform.GetComponentsInChildren<Weakpoint>();
    rgd = GetComponent<Rigidbody>();
    EnterDictionary();
  }

  [Command]
  void CmdPrintName()
  {
    print(transform.name + " is alive.");
  }

  [ClientRpc]
  public void RpcTriggerHealthUpdate(float v)
  {
    hudManager.UpdateHealthBar(v);
  }

  [Server]
  void EnterDictionary()
  {
    if (Enemy.EnemyDictionary == null)
    {
      Enemy.EnemyDictionary = new Dictionary<string, Enemy>();
    }
    Enemy.EnemyDictionary.Add(transform.name, this);
  }

  [Server]
  public void InflictDamage(float damage, bool hitWeakpoint, string playerID)
  {
    if (!inCombat){
      inCombat = true;
    }
    CurrentHealth -= damage;
    if (CurrentHealth <= 0)
    {
      Enemy.EnemyDictionary.Remove(transform.name);
      NetworkServer.Destroy(gameObject);
    }
    if (hitWeakpoint){
      WeakpointAction(playerID);
    }
    RpcTriggerHealthUpdate(HealthPercent);
  }

  [Server]
  protected virtual void WeakpointAction(string playerID){

  }
}
