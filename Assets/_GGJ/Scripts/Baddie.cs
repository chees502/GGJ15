using UnityEngine;
using System.Collections;

public class Baddie : MonoBehaviour {
    public Transform target;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 dir = target.position-transform.position;
        float rotAmount = Vector3.Dot(transform.right, dir.normalized);
        transform.Rotate(0, rotAmount * Time.deltaTime*50, 0);
	}
}
