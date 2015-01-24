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

    void Awake() {

    }

    void Update()
    {
        // Get forward/right input
        Vector3 moveVector = new Vector3(
            Input.GetAxis("Horizontal") * horizontalAccel,
            0f,           
            Input.GetAxis("Vertical") * verticalAccel);

        // Add forward/right input to velocity
        rigidbody.velocity += transform.TransformDirection(moveVector);

        // Limit the forward/right velocity
        Vector2 frLimitied = new Vector2(rigidbody.velocity.x, rigidbody.velocity.z);
        if (frLimitied.magnitude > maxForwardRightVel) {
            frLimitied = frLimitied.normalized * maxForwardRightVel;
        }
        rigidbody.velocity = new Vector3(frLimitied.x, rigidbody.velocity.y, frLimitied.y);

        // Rotates towards the camera's facing direction
        // Only when the character is moving
        if (moveVector.magnitude > 0) {
            Quaternion targetRot = Quaternion.LookRotation((new Vector3(cameraTrans.forward.x, 0, cameraTrans.forward.z)).normalized);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, rotationRate * Time.deltaTime);
        }

        // Handle Jumping
        if (Input.GetButtonDown("Jump"))
        {

        }
    }

    
    void OnTriggerEnter(Collider other) {
        Debug.Log("Trigger entered");
    }


}
