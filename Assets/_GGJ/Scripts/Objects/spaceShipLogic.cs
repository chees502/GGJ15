using UnityEngine;
using System.Collections;

public class spaceShipLogic : MonoBehaviour {
    Transform parent;
    public Bounds bound;
    Vector3 target;
	// Use this for initialization
	void Start () {
        parent = transform.parent;
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(0, Time.deltaTime*20, 0);
        parent.position += parent.forward * Time.deltaTime*5;
        Vector3 dir = target - parent.position;
	}
}
