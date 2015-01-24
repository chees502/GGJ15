using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
    public bool playerOne = true;
	// Use this for initialization
	void Start () {
        if (playerOne)
        {
            _root.p1 = this;
        }
        else
        {
            _root.p2 = this;
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (playerOne)
        {
            Move("w",       0,10);
            Move("a",       -10,0);
            Move("s",       0,-10);
            Move("d",       10,0);
        }
        else
        {
             Move("up",    0, 10);
             Move("left",  -10, 0);
             Move("down",  0, -10);
             Move("right", 10, 0);
        }
	}

    void Move(string key, float hori, float vert)
    {
        if (Input.GetKey(key))
        {
            transform.position += ((Camera.main.transform.up * vert) + (Camera.main.transform.right * hori)) * Time.deltaTime;
        }
    }
}
