using UnityEngine;
using System.Collections;

public class Tether : MonoBehaviour {
    public float Distance = 6;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
    void LateUpdate()
    {
        Vector3 p1Pos = _root.p1.transform.position;
        Vector3 p2Pos = _root.p2.transform.position;
        Vector3 p1Top2 = p2Pos - p1Pos;
        float currentDist = p1Top2.magnitude;
        Vector3 center = p1Pos + p1Top2 * 0.5f;
        Debug.DrawLine(p1Pos, center);
        _root.p1.transform.position = center + p1Top2.normalized * -0.5f * Distance;
        _root.p2.transform.position = center + p1Top2.normalized * 0.5f * Distance;
        _root.bar.transform.position = center;
        _root.bar.transform.rotation = Quaternion.Euler(new Vector3(0, Mathf.Atan2(p1Top2.x, p1Top2.z)*114*0.5f+90,0));
	}
}
