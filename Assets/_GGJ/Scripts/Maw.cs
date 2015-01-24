using UnityEngine;
using System.Collections;

public class Maw : MonoBehaviour {

	// Use this for initialization
    public bool mouthOpen = false;
    public float xRot = 90;
	void Awake () {
        _Dog.maw = this;
	}
    void Update()
    {
        if (mouthOpen)
        {
            xRot=Mathf.Lerp(xRot, 65, Time.deltaTime*4);
        }
        else
        {
            xRot = Mathf.Lerp(xRot, 90, Time.deltaTime*8);
        }
        transform.localRotation = Quaternion.Euler(xRot, 0, 0);
    }
}
