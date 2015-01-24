using UnityEngine;
using System.Collections;

public class peeControl : MonoBehaviour {

	static public bool isPissing;
	static public float pissDistance = 0.0f;
	static public float rateOfFire = 0.1f;
	public float fireTime = 0.0f;

	public float testFloat = 0.0f;
	public bool isPressed;

	GameObject pee;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
//		if (Input.GetMouseButtonDown (0)) {

		if(Input.GetButton("Fire1")){
			pissDistance += 1.0f;
			if(Time.time > fireTime+rateOfFire){
				pee = Instantiate (Resources.Load ("projectile_pee"), transform.position, transform.rotation) as GameObject;
				isPissing = true;

			
				testFloat += 1.0f;
				isPressed = true;
				fireTime = Time.time;
			}
		}



		if (Input.GetMouseButtonUp (0)) {
			isPissing = false;
			pissDistance = 0.0f;
			testFloat = 0.0f;
			isPressed = false;
			Debug.Log(isPissing);
		}
	}
}
