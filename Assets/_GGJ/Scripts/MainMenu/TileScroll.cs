using UnityEngine;
using System.Collections;

public class TileScroll : MonoBehaviour {
    Renderer myRender;
	// Use this for initialization
	void Start () {
        myRender = transform.GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void Update () {
        myRender.material.SetTextureOffset("_MainTex", new Vector2(Time.time, 0));
	}
}
