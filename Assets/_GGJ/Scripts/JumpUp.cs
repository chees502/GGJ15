using UnityEngine;
using System.Collections;

public class JumpUp : MonoBehaviour {
    public Transform EyeSensor;
    public Transform HaloSensor;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}
    bool CanJump(out RaycastHit spot)
    {

        //cast from face
        if (Physics.Raycast(new Ray(EyeSensor.position, EyeSensor.forward), 1))
        {
            Debug.DrawRay(EyeSensor.position, EyeSensor.forward, Color.red);
            //cast if there is space to vault
            if (Physics.Raycast(new Ray(HaloSensor.position, HaloSensor.forward * 2), 2))
            {
                Debug.DrawRay(HaloSensor.position, HaloSensor.forward * 2, Color.red);
            }
            else
            {
                Debug.DrawRay(HaloSensor.position, HaloSensor.forward * 2, Color.green);
                RaycastHit hit = new RaycastHit();
                //cast for surface to jump to
                if (Physics.Raycast(new Ray(HaloSensor.position, (HaloSensor.forward + -HaloSensor.up) * 1.5f), out hit, 2.5f))
                {
                    Debug.DrawRay(HaloSensor.position, (HaloSensor.forward + -HaloSensor.up) * 1.5f);
                    //cast if surface is standable
                    if (Vector3.Dot(hit.normal, Vector3.up) > 0.9f)
                    {
                        Debug.DrawRay(hit.point, hit.normal, Color.blue);
                        spot = hit;
                        return true;
                    }
                    else
                    {
                        Debug.DrawRay(hit.point, hit.normal, Color.red);
                    }
                }
                else
                {
                    Debug.DrawRay(HaloSensor.position, (HaloSensor.forward + -HaloSensor.up) * 1.5f, Color.red);
                }
            }
        }
        else
        {
            Debug.DrawRay(EyeSensor.position, EyeSensor.forward);
        }
        spot = new RaycastHit();
        return false;
    }
    public void Jump()
    {
        RaycastHit spot = new RaycastHit();
        if (CanJump(out spot))
        {
            _Dog.Dog.transform.position = spot.point+new Vector3(0,1,0);
        }
    }
}
