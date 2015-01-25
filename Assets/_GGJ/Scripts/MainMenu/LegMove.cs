using UnityEngine;
using System.Collections;

public class LegMove : MonoBehaviour {
    public float offset = 3.14f;
    public float rotOffset = 0;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 oldRot = transform.rotation.eulerAngles;
        oldRot.z = 65+rotOffset + Mathf.Sin((Time.time+offset)*7) * 30;
        transform.rotation = Quaternion.Euler(oldRot);
	}
}
