using UnityEngine;
using System.Collections;

public class Grabber : MonoBehaviour {
	public bool holding=false;
    public Transform heldItem;
    public SpringJoint heldItemJoint;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Debug.DrawRay (transform.position, transform.forward * 2,Color.green);
        RaycastHit hit = new RaycastHit();
        if (holding==false&&
            Physics.Raycast(new Ray(transform.position, transform.forward), out hit, 2) &&
            hit.transform.gameObject.GetComponent<Rigidbody>())
        {
            _Dog.maw.mouthOpen = true;
        }
        else
        {
            _Dog.maw.mouthOpen = false;

        }

        if(Input.GetKeyDown("e")){
            if (holding)
            {
                Release();
            }
            else
            {
                Grab();
            }
		}
	}
    void Grab()
    {       
        RaycastHit hit = new RaycastHit();
        //cast for surface to jump to
        if (Physics.Raycast(new Ray(transform.position,transform.forward), out hit, 2))
        {
            if (hit.transform.gameObject.GetComponent<Rigidbody>())
            {
                heldItem = hit.transform;
                holding = true;
                heldItemJoint = hit.transform.gameObject.AddComponent<SpringJoint>();
                heldItemJoint.connectedBody = gameObject.GetComponent<Rigidbody>();
                heldItemJoint.spring = 30;
                heldItemJoint.maxDistance = 0;
            }
        }
    }
    void Release()
    {
        holding = false;
        Destroy(heldItemJoint);
    }
}
