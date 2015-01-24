using UnityEngine;
using System.Collections;

public class Maw : MonoBehaviour {

	// Use this for initialization
    public bool mouthOpen = false;
	void Awake () {
        _Dog.maw = this;
	}
    void Update()
    {
        if (mouthOpen)
        {

        }
        else
        {

        }
    }
}
