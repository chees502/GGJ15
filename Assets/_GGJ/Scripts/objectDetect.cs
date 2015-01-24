﻿using UnityEngine;
using System.Collections;

public class objectDetect : MonoBehaviour {
	asset_deform deformScript;
	static public Vector3 playerPos;
	public bool isStationary;
	public bool isDestructable;

	// Use this for initialization

	void Awake(){
		deformScript = GetComponentInChildren<asset_deform> ();
		if(gameObject.GetComponent<Rigidbody>() == null){
			gameObject.AddComponent<Rigidbody> ();
		}
		//		gameObject.rigidbody.freezeRotation = true;
		if(isStationary){
			gameObject.rigidbody.constraints = RigidbodyConstraints.FreezeAll;
		}

	}
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision collision) {
		if(collision.gameObject.tag == "Player" ||
		   collision.gameObject.tag == "Projectiles"){
			foreach (ContactPoint contact in collision.contacts) {
				Debug.DrawRay(contact.point, contact.normal, Color.white);
				Debug.Log(contact.point);
			}
			deformScript.bHit = true;
			Debug.Log ("Collide");
		}
	}

	void OnCollisionExit(){
		deformScript.bHit = false;
	}
}
