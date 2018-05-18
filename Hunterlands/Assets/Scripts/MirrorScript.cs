using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorScript : MonoBehaviour {
  //[SerializeField]
  //GameObject targettedMirror;

  RenderTexture tex;
  Material mat;

	// Use this for initialization
	void Start () {
    tex = new RenderTexture(1024, 1024, 24);
    GetComponent<Camera>().targetTexture = tex;
    mat = transform.parent.GetComponent<MeshRenderer>().material;
    mat.mainTexture = tex;
    tex.wrapMode = TextureWrapMode.Repeat;
    mat.mainTextureScale = new Vector2(-1, 1);
  }
}
