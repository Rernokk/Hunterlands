using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(Rigidbody))]
public class Player_Controller : NetworkBehaviour
{
  Player_Camera_Controller camCtrl;
  Weapon_Class currentWeapon;
  Rigidbody rgd;

  [SerializeField]
  GameObject bulletVFX;

  [SerializeField]
  LayerMask targetMask;

  public Weapon_Class CurrentWeapon
  {
    get
    {
      return currentWeapon;
    }

    set
    {
      currentWeapon = value;
    }
  }

  void Awake()
  {
    camCtrl = GetComponent<Player_Camera_Controller>();
    rgd = GetComponent<Rigidbody>();
    if (!isLocalPlayer)
    {
      gameObject.layer = LayerMask.NameToLayer("RemotePlayer");
    }
  }

  private void Start()
  {
    transform.name = "Player_" + GetComponent<NetworkIdentity>().netId;
  }

  // Update is called once per frame
  void Update()
  {
    if (!isLocalPlayer)
    {
      return;
    }

    //Only process while cursor is locked.
    if (!Cursor.visible || true)
    {
      //Player Shooting
      if (Input.GetKeyDown(KeyCode.Mouse0))
      {
        print("Registered mouse down");
        //RaycastHit info;
        if (currentWeapon != null)
        {
          if (camCtrl.CamMode == CameraMode.FIRST)
          {
            //  Ray tarRay = camCtrl.CurrentCamera.GetComponent<Camera>().ViewportPointToRay(new Vector3(0.5f, 0.5f));
            //  Physics.Raycast(tarRay, out info, 80f);
            //  Debug.DrawRay(tarRay.origin, tarRay.direction * 12f, Color.red, 1f);
            //  if (info.transform != null)
            //  {
            //    print(info.transform.name + ", " + info.collider.tag + " Hit!");
            //    //Target_Debug_Script tarScr = info.transform.GetComponent<Target_Debug_Script>();
            //    Enemy tarScr = info.transform.GetComponentInParent<Enemy>();
            //    Destroy(Instantiate(bulletVFX, info.point, Quaternion.Euler(info.normal)), 2f);
            //    if (tarScr != null)
            //    {
            //      //tarScr.TriggerHitInfo(10);
            //      print("First person hit, triggering command.");
            //      tarScr.CmdTriggerDamage(10);
            //    }
            //  }

            Ray tarRay = camCtrl.CurrentCamera.GetComponent<Camera>().ViewportPointToRay(new Vector3(0.5f, 0.5f));
            ShootFP(tarRay);
          }
          else if (camCtrl.CamMode == CameraMode.THIRD)
          {
            Ray tarRay = camCtrl.CurrentCamera.GetComponent<Camera>().ViewportPointToRay(new Vector3(0.5f, 0.5f));
            ShootTP(tarRay);
          }
        }
      }
      Vector3 directionOfMotion = Vector3.zero;
      if (Input.GetKey(KeyCode.A))
      {
        //transform.position -= transform.right * Time.deltaTime * 5f;
        directionOfMotion -= transform.right;
      }
      if (Input.GetKey(KeyCode.D))
      {
        //transform.position += transform.right * Time.deltaTime * 5f;
        directionOfMotion += transform.right;
      }
      if (Input.GetKey(KeyCode.S))
      {
        //transform.position -= transform.forward * Time.deltaTime * 5f;
        directionOfMotion -= transform.forward;
      }
      if (Input.GetKey(KeyCode.W))
      {
        //transform.position += transform.forward * Time.deltaTime * 5f;
        directionOfMotion += transform.forward;
      }

      rgd.velocity = directionOfMotion.normalized * 15f;

      if (Input.GetKey(KeyCode.E))
      {
        transform.Rotate(new Vector3(0, 180 * Time.deltaTime, 0));
      }
      if (Input.GetKey(KeyCode.Q))
      {
        transform.Rotate(new Vector3(0, -180 * Time.deltaTime, 0));
      }

      if (Input.GetKeyDown(KeyCode.Equals))
      {
        Application.Quit();
      }
    }
    else
    {
      if (Input.GetKeyDown(KeyCode.Mouse0))
      {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
      }
    }
  }

  [Command]
  void CmdDamageTarget(WeaponType type, string id, float critMulti, bool hitWeakpoint)
  {
    print("Target hit: " + id + " with " + type.ToString() + " at " + critMulti + "x damage");
    Enemy.EnemyDictionary[id].InflictDamage(10f * critMulti, hitWeakpoint, transform.name);
  }

  [Client]
  private void ShootTP(Ray tarRay)
  {
    RaycastHit info;
    Physics.Raycast(tarRay, out info, 80f);
    if (info.transform != null)
    {
      float alignmentVal = Vector3.Dot((transform.forward).normalized, (info.point - transform.position).normalized);
      if (alignmentVal > .5f)
      {
        Enemy tarScr = info.transform.GetComponentInParent<Enemy>();
        Destroy(Instantiate(bulletVFX, info.point, Quaternion.Euler(info.normal)), 2f);
        if (tarScr != null)
        {
          if (info.collider.GetComponent<Weakpoint>() && info.collider.GetComponent<Weakpoint>().CompareWeaponType(currentWeapon.wepType))
          {
            CmdDamageTarget(currentWeapon.wepType, tarScr.transform.name, info.collider.GetComponent<Weakpoint>().CritMultiplier, true);
          }
          else
          {
            CmdDamageTarget(currentWeapon.wepType, tarScr.transform.name, 1.0f, false);
          }
        }
      }
    }
  }

  [Client]
  void ShootFP(Ray tarRay)
  {
    RaycastHit info;
    Physics.Raycast(tarRay, out info, 80f, targetMask);
    if (info.transform != null)
    {
      Enemy tarScr = info.transform.GetComponentInParent<Enemy>();
      Destroy(Instantiate(bulletVFX, info.point, Quaternion.Euler(info.normal)), 2f);
      if (tarScr != null)
      {
        if (info.collider.GetComponent<Weakpoint>() && info.collider.GetComponent<Weakpoint>().CompareWeaponType(currentWeapon.wepType))
        {
          CmdDamageTarget(currentWeapon.wepType, tarScr.transform.name, info.collider.GetComponent<Weakpoint>().CritMultiplier, true);
        }
        else
        {
          CmdDamageTarget(currentWeapon.wepType, tarScr.transform.name, 1.0f, false);
        }
      }
    }
  }

  //A note about ClientRPCs and Commands:
  /*  Client Tags are only called on the client.
   *  Commands are used to tell the server what the client wants to do. It only can be executed with local player authority.
   *  ClientRPCs are the opposite - they are executed by the server on every client to inform each player what the server's decision was.
   *  TargetRPCs can be used to specifically execute a command on one client (If one may need that)
   */
}
