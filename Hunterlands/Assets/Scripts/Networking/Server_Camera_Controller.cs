using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Server_Camera_Controller : NetworkBehaviour
{
  [SerializeField]
  float cameraSpeed = 5f, cameraRotSpeed = 60f;
  // Use this for initialization
  void Start()
  {
  }

  public override void OnStartServer()
  {
    print("Server Started");
  }

  public override void OnStartClient()
  {
    gameObject.SetActive(false);
    print("Client Started");
  }

  // Update is called once per frame
  void Update()
  {
    if (!isServer)
    {
      return;
    }

    if (Input.GetKey(KeyCode.A))
    {
      transform.parent.position -= transform.right * Time.deltaTime * cameraSpeed;
    }
    if (Input.GetKey(KeyCode.D))
    {
      transform.parent.position += transform.right * Time.deltaTime * cameraSpeed;
    }
    if (Input.GetKey(KeyCode.W))
    {
      transform.parent.position += transform.forward * Time.deltaTime * cameraSpeed;
    }
    if (Input.GetKey(KeyCode.S))
    {
      transform.parent.position -= transform.forward * Time.deltaTime * cameraSpeed;
    }
    if (Input.GetKey(KeyCode.Q))
    {
      transform.Rotate(new Vector3(0, -cameraRotSpeed * Time.deltaTime));
    }
    if (Input.GetKey(KeyCode.E))
    {
      transform.Rotate(new Vector3(0, cameraRotSpeed * Time.deltaTime));
    }
    if (Input.GetKey(KeyCode.R))
    {
      transform.parent.Rotate(new Vector3(-cameraRotSpeed * Time.deltaTime, 0));
    }
    if (Input.GetKey(KeyCode.F))
    {
      transform.parent.Rotate(new Vector3(cameraRotSpeed * Time.deltaTime, 0));
    }

    if (Input.GetKeyDown(KeyCode.C))
    {
      GetComponent<AudioListener>().enabled = !GetComponent<AudioListener>().enabled;
    }
  }
}
