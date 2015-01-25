using UnityEngine;
using System.Collections;

public class CreditName : MonoBehaviour {
    float velocity = 0;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        velocity += Time.deltaTime*2;
        transform.Translate(-velocity*Time.deltaTime, 0, 0);
        if (transform.position.y < -10)
        {
            Destroy(gameObject);
        }
	}
    public void setName(string name)
    {
        gameObject.GetComponent<TextMesh>().text = name;
    }
}
