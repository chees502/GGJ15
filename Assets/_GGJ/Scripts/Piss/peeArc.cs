using UnityEngine;
using System.Collections;

public class peeArc : MonoBehaviour {

	public bool bPiss;
	public float pissDistance = 0.0f;
	public float pissDistanceMax = 10.0f;
	public float pissSpeed = 2.0f;
	public float killTime = 5.0f;

	bool stopBezier = false;

	ParticleSystem splash;

	float BezierTime = 0.0f;

//	float CurveX;
//	float CurveY;

	public Vector3 startPos = new Vector3 ();// Vector3.zero;
	public Vector3 controlPos = new Vector3 ();// Vector3.zero;
	public Vector3 endPos = new Vector3 ();//  Vector3.zero;
	public Vector3 curveVec = new Vector3 ();//  Vector3.zero;

	float dist;
	// Use this for initialization
	void Start () {
		StartCoroutine ("killTimer");
		peeDistanceCalc ();
	//	startPos = transform.root.position;
	//	endPos = transform.root.position + transform.root.forward * pissDistance;
	//	dist = Vector3.Distance (startPos, endPos);
	//	controlPos = transform.root.position + transform.root.forward * pissDistance * 0.5f + transform.root.up * pissDistance * 0.5f;
	}
	
	// Update is called once per frame
	void Update () {

		//if (peeControl.isPissing) {
		if(!stopBezier){
			bezierCalc();
		}
		//}
	}
	void peeDistanceCalc(){
		pissDistance = peeControl.pissDistance;
		if (pissDistance > pissDistanceMax) {
			pissDistance = pissDistanceMax;		
		}
		startPos = transform.root.position;
		endPos = transform.root.position + transform.root.forward * pissDistance;
		dist = Vector3.Distance (startPos, endPos);
		controlPos = transform.root.position + (transform.root.forward * (pissDistance * 0.5f)) + (transform.root.up * (pissDistance * 0.25f));
	}
	void bezierCalc(){
		BezierTime = BezierTime + (Time.deltaTime * pissSpeed);
		if (BezierTime == 1)
		{
			BezierTime = 0;
		}
		curveVec.x = (((1-BezierTime)*(1-BezierTime)) * startPos.x) + (2 * BezierTime * (1 - BezierTime) * controlPos.x) + ((BezierTime * BezierTime) * endPos.x);
		curveVec.y = (((1-BezierTime)*(1-BezierTime)) * startPos.y) + (2 * BezierTime * (1 - BezierTime) * controlPos.y) + ((BezierTime * BezierTime) * endPos.y);
		curveVec.z = (((1-BezierTime)*(1-BezierTime)) * startPos.z) + (2 * BezierTime * (1 - BezierTime) * controlPos.z) + ((BezierTime * BezierTime) * endPos.z);
		transform.position += curveVec.normalized;
	
	}

	IEnumerator killTimer(){
		yield return new WaitForSeconds (killTime);
		Destroy (gameObject);
	}

	void OnCollisionEnter(Collision col){
		if (col.transform.tag != "Player" || col.transform.tag != "Projectiles"){
	//	stopBezier = true;
			Vector3 dirHit = new Vector3 ();
			dirHit = col.transform.position - this.transform.position;
		
			Quaternion hitRot = new Quaternion();
			hitRot = Quaternion.LookRotation (dirHit);
		//	Vector3 dirHitRot = new Vector3 (dirHit.x * 360.0f, dirHit.y * 360.0f, dirHit.z * 360.0f);
			Debug.Log (hitRot);
			splash = Instantiate (Resources.Load ("psPeeSplash"), transform.position, hitRot) as ParticleSystem;
			Destroy (gameObject);
		}
	}
}
