using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameSetupScript : NetworkBehaviour
{
  [SerializeField]
  List<GameObject> registerPrefabs;

  // Use this for initialization
  void Start()
  {
    foreach (GameObject go in registerPrefabs){
      ClientScene.RegisterPrefab(go);
    }
  }

}
