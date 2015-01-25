using UnityEngine;
using System.Collections;

public class spaceShipLogic : MonoBehaviour {
    Transform parent;
    public Bounds bound;
    Vector3 target;
    Vector3 targetMove;
	// Use this for initialization
	void Start () {
        parent = transform.parent;
        NewTarget();
	}
	
	// Update is called once per frame
	void Update () {

        transform.Rotate(0, Time.deltaTime*20, 0);
        parent.position += parent.forward * Time.deltaTime*5;
        target += targetMove * Time.deltaTime;
        Vector3 dir = target - parent.position;
        Debug.DrawLine(parent.position, target, Color.blue);
        if (Vector3.Distance(parent.position, target) < 5)
        {
            NewTarget();
        }
        float turn = Vector3.Dot(parent.right, dir);
        parent.Rotate(0, turn*Time.deltaTime, 0);
	}
    void NewTarget()
    {
        target.x = Random.Range(bound.min.x, bound.max.x);
        target.z = Random.Range(bound.min.z, bound.max.z);
        targetMove = Random.onUnitSphere;
        targetMove.y = 0;
        targetMove.Normalize();
    }
}
