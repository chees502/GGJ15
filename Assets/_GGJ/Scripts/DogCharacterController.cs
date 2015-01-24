#define Temp

using UnityEngine;
using System.Collections;

[RequireComponent(typeof(JumpUp))]
public class DogCharacterController : MonoBehaviour {
    static private DogCharacterController _instance;
    static public DogCharacterController Instance {
        get { return _instance; }
    }

    public JumpUp jumpController = null;
    public float jumpForce = 30f;
    public ForceMode jumpForceMode = ForceMode.Impulse;
    public float jumpInputModifier = 0.1f;

    public float cameraLookRate = 1f;

    public float verticalAccel = 1;
    public float horizontalAccel = 1;

    public float maxForwardRightVel = 4;

    private Vector3 targetLocation;

    private Vector3 moveVector;

    private float distToGround = 1f;
    private float groundedMinSlope = 0.7f;

    private bool isGrounded = false;

    void Awake() {
        if (DogCharacterController._instance == null) {
            DogCharacterController._instance = this;
        } else {
            Debug.LogWarning("[DogCharacterController]: More then one instance of the script in the scene");
        }

        jumpController = GetComponent<JumpUp>();


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
            case _Dog._DogState.Urinating:
                Urinate();
                break;
        }
    }

    void LateUpdate()
    {
        isGrounded = false;
    }

    void Idle()
    {
        // Get the input and apply acceleration rates
        moveVector = new Vector3(
            Input.GetAxis("Horizontal") * horizontalAccel,
            0f,
            Input.GetAxis("Vertical") * verticalAccel);

        // Apply the jumpInputModifier to the move Vector is not grouned
        moveVector *= isGrounded ? 1f : jumpInputModifier;

        // Change move vector into object space
        moveVector = transform.TransformDirection(moveVector);

        // Add move vector to rigidbodies velocity as long as the velocity
        // is below the max velocity to apply input
        Vector2 frLimitied = new Vector2(rigidbody.velocity.x, rigidbody.velocity.z);
        if ((frLimitied + new Vector2(moveVector.x, moveVector.z)).magnitude < maxForwardRightVel) {
            rigidbody.velocity += moveVector;
        }

        // Rotate the based off the mouse x input
        transform.Rotate(Vector3.up, Input.GetAxis("Mouse X") * cameraLookRate * Time.deltaTime);

        // Handle Jumping
        if (Input.GetButtonDown("Jump")) {
            Jump();
        }
    }

    void Urinate()
    {

    }

    bool IsGrounded()
    {
        RaycastHit hitInfo;
        Ray ray = new Ray(transform.position, Vector3.down);
        if (Physics.Raycast(ray, out hitInfo, distToGround + 0.1f))
        {
            if (Vector3.Dot(hitInfo.normal, Vector3.up) > groundedMinSlope)
            {
                return true;
            }
        }
        return false;
    }


    void Jump()
    {
        RaycastHit spot = new RaycastHit();
        if (jumpController.CanJump(out spot))
        {
            targetLocation = spot.point + new Vector3(0, 1, 0);
            _Dog.DogState = _Dog._DogState.Climbing;
        }
        else if (isGrounded)
        {
            Vector3 jumpForceDir = Vector3.up;
            jumpForceDir += moveVector.normalized;
            jumpForceDir *= jumpForce;
            rigidbody.AddForce(jumpForceDir, jumpForceMode);

            Debug.DrawRay(transform.position, jumpForceDir, Color.red);
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

    void OnCollisionStay(Collision collision)
    {
        foreach (var contact in collision.contacts)
        {
            // Check if the dot of the contact point is above the min slope
            if (Vector3.Dot(contact.normal, Vector3.up) > groundedMinSlope)
            {
                isGrounded = true;
            }
        }
    }
}
