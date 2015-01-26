using UnityEngine;
using System.Collections;

public class peeControl : MonoBehaviour {

	static public bool isPissing;
	static public float pissDistance = 0.0f;
	static public float rateOfFire = 0.05f;
	public float fireTime = 0.0f;
	static public Vector3 muzzlePos = new Vector3 ();
//	public float testFloat = 0.0f;
//	public bool isPressed;

	GameObject pee;
	// Use this for initialization

	void Awake(){
		_Dog.OnDogStateChange += OnDogStageChange;
	}
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		muzzlePos = transform.position;
//		if (Input.GetMouseButtonDown (0)) {
	//	if(Input.GetButton("Fire1")){
		if(isPissing){
			pissDistance += 1.0f;
			if(Time.time > fireTime+rateOfFire){
				pee = Instantiate (Resources.Load ("projectile_pee"), transform.position, transform.rotation) as GameObject;
				isPissing = true;
							

				fireTime = Time.time;
			}
		}



	//	if (Input.GetMouseButtonUp (0)) {
	//		isPissing = false;
	//		pissDistance = 0.0f;
	//		Debug.Log(isPissing);
	//	}
	}

	void OnDogStageChange( _Dog._DogState newState ){
		if (newState == _Dog._DogState.Urinating) {
			isPissing = true;
		}
		else{
			isPissing = false;
			pissDistance = 0.0f;
			Debug.Log(isPissing);
		}
	}
}
