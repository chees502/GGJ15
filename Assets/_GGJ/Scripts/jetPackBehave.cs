using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class jetPackBehave : MonoBehaviour {

	public enum _jetPack_states{idle, touched, flying, endFlight, dead}
	public _jetPack_states _current_state;

	public Vector3 startPos = new Vector3 ();// Vector3.zero;
	public List<Vector3> controlPos = new List<Vector3> ();// Vector3.zero;
	public bool autoGenPoints;
	public int howManyPoints;
	public List<Vector3> endPos = new List<Vector3> ();//  Vector3.zero;
	public Vector3 curveVec = new Vector3 ();//  Vector3.zero;

	float BezierTime = 0.0f;
	int currentPoint;
	public float travelSpeed = 20.0f;
	public float turnSpeed = 2.0f;
	Vector3 currentDest = new Vector3();

	public bool testFlight;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (testFlight) {
			_current_state = _jetPack_states.touched;
			testFlight = false;
		}
		StateManager ();
	}

	void StateManager(){
		if (_current_state == _jetPack_states.idle) {
			currentPoint = 0;
		}
		if (_current_state == _jetPack_states.touched) {
			startPos = transform.position;
			if(autoGenPoints){
				createDestinationPoints(howManyPoints);
				createControlPoints();
			}
		//	else{ howManyPoints = endPos.Count; }
			_current_state = _jetPack_states.flying;
		}
		if (_current_state == _jetPack_states.flying) {
		//	bezierCalc(howManyPoints);
			travel();
		
		}
		if (_current_state == _jetPack_states.endFlight) {
			travelSpeed = 0.0f;
			gameObject.AddComponent<Rigidbody>();

			_current_state = _jetPack_states.dead;
		}
		if (_current_state == _jetPack_states.dead) {

		}
	}
	void createDestinationPoints(int points){
		Vector3 point = startPos;
		for (int i = 0; i<points; i++) {
			float x = Random.Range(point.x - 200.0f, point.x + 200.0f);
			float y = Random.Range(point.y, point.y + 20.0f);
			float z = Random.Range(point.z - 200.0f, point.z + 200.0f);
			endPos.Add (new Vector3(x,y,z));
			point = endPos[i];
		}
	}

	void createControlPoints(){
		Vector3 point = startPos;
		for (int i = 0; i< endPos.Count; i++) {
			float x = Random.Range(point.x,endPos[i].x);
			float y = Random.Range(point.y,endPos[i].y);
			float z = Random.Range(point.z,endPos[i].z);
			controlPos.Add (new Vector3(x,y,z));
			point = endPos[i];
		}
	}

//	void bezierCalc( int points){
//
//		BezierTime = BezierTime + (Time.deltaTime * travelSpeed);
//
//		curveVec.x = (((1-BezierTime)*(1-BezierTime)) * startPos.x) + (2 * BezierTime * (1 - BezierTime) * controlPos[currentPoint].x) + ((BezierTime * BezierTime) * endPos[currentPoint].x);
//		curveVec.y = (((1-BezierTime)*(1-BezierTime)) * startPos.y) + (2 * BezierTime * (1 - BezierTime) * controlPos[currentPoint].y) + ((BezierTime * BezierTime) * endPos[currentPoint].y);
//		curveVec.z = (((1-BezierTime)*(1-BezierTime)) * startPos.z) + (2 * BezierTime * (1 - BezierTime) * controlPos[currentPoint].z) + ((BezierTime * BezierTime) * endPos[currentPoint].z);
//	//	transform.position += curveVec.normalized * Time.deltaTime;
//		transform.position = curveVec;
//		transform.LookAt (endPos [currentPoint]);
//		if (BezierTime == 1 && currentPoint < howManyPoints){
//			BezierTime = 0;
//			startPos = endPos[currentPoint];
//			currentPoint++;
//		}
//		if (currentPoint == howManyPoints){
//			_current_state = _jetPack_states.endFlight;
//		}
//
//	}

	void travel(){
		currentDest = endPos [currentPoint];
	//	transform.position = Vector3.Slerp (transform.position, currentDest, travelSpeed * Time.deltaTime);
	//	transform.position = Vector3.MoveTowards (transform.position, currentDest, travelSpeed * Time.deltaTime);
		transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (currentDest - transform.position), turnSpeed * Time.deltaTime);
		transform.position += transform.forward * travelSpeed * Time.deltaTime;
		if (Vector3.Distance (transform.position, currentDest) < 20.0f) {
			currentPoint++;		
		}
		if (currentPoint >= howManyPoints) {
			_current_state = _jetPack_states.endFlight;		
		}
	
	}



}
