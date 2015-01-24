#define Temp

using UnityEngine;
using System.Collections;

public class DogCharacterController : MonoBehaviour {
    public float CameraLookRate = 1f;

    public Transform cameraTrans;

    public float verticalAccel;
    public float horizontalAccel;

    public float maxForwardRightVel = 1000;

    public float rotationRate = 10f;

    public Vector3 moveVector;
    public Vector3 targetLocation;
    void Awake() {
        _Dog.Dog = gameObject;
        _Dog.BuildDogConnection();
    }

    void Update()
    {
        switch (_Dog.DogState)
        {
            case _Dog._DogState.Idle:
                Idle();
                break;
            case _Dog._DogState.Climbing:
                Climb();
                break;
        }
    }
    void Idle()
    {

        // Get forward/right input
        moveVector = new Vector3(
            Input.GetAxis("Horizontal") * horizontalAccel,
            0f,
            Input.GetAxis("Vertical") * verticalAccel);

        // Add forward/right input to velocity
        rigidbody.velocity += transform.TransformDirection(moveVector);

        // Limit the forward/right velocity
        Vector2 frLimitied = new Vector2(rigidbody.velocity.x, rigidbody.velocity.z);
        if (frLimitied.magnitude > maxForwardRightVel)
        {
            frLimitied = frLimitied.normalized * maxForwardRightVel;
        }
        rigidbody.velocity = new Vector3(frLimitied.x, rigidbody.velocity.y, frLimitied.y);

        // Rotates towards the camera's facing direction
        // Only when the character is moving
        if (moveVector.magnitude > 0)
        {
            Quaternion targetRot = Quaternion.LookRotation((new Vector3(_Dog.Camera.transform.forward.x, 0, _Dog.Camera.transform.forward.z)).normalized);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, rotationRate * Time.deltaTime);
        }

        // Handle Jumping
        if (Input.GetButtonDown("Jump"))
        {
            gameObject.GetComponent<JumpUp>().Jump();
        }
    }
    void Climb()
    {
        transform.position = Vector3.Lerp(transform.position, targetLocation, Time.deltaTime*5);
        if(Vector3.Distance(transform.position,targetLocation)<0.1f){
            _Dog.DogState=_Dog._DogState.Idle;
        }
    }
    
    void OnTriggerEnter(Collider other) {
        Debug.Log("Trigger entered");
    }


}
