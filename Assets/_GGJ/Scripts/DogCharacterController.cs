#define Temp

using UnityEngine;
using System.Collections;

[RequireComponent(typeof(JumpUp))]
public class DogCharacterController : MonoBehaviour {
    static private DogCharacterController _instance;
    static public DogCharacterController Instance {
        get { return _instance; }
    }

    public float verticalAccel = 1;
    public float horizontalAccel = 1;

    public float idleMaxForwardRightVel = 4f;
    public float idleInputModifier = 1f;

    public JumpUp jumpController = null;
    public float jumpForce = 30f;
    public ForceMode jumpForceMode = ForceMode.Impulse;
    public float jumpInputModifier = 0.1f;

    public float urinInputModifier = 0.3f;
    public float urinRotationModifier = 0.5f;
    public float urinMaxForwardRightVel = 2f;

    public float cameraLookRate = 1f;

    private Vector3 targetLocation;
    private float startTime;
    private Vector3 moveVector;

    private float distToGround = 1f;
    private float groundedMinSlope = 0.7f;

    private bool _isGrounded = false;

    public string InputAxisHorizontal = "Horizontal";
    public string InputAxisVertical = "Vertical";
    public string InputAxisRotateY = "Mouse X";
    public string InputButtonJump = "Jump";
    public string InputButtonFire = "Fire1";

    public bool IsGrouned
    {
        get { return _isGrounded; }
    }

    void Awake() {
        if (DogCharacterController._instance == null) {
            DogCharacterController._instance = this;
        } else {
            Debug.LogWarning("[DogCharacterController]: More then one instance of the script in the scene");
        }

        jumpController = GetComponent<JumpUp>();


        _Dog.Dog = gameObject;
        _Dog.BuildDogConnection();

        _Dog.OnDogStateChange += OnDogStateChanged;
    }

    void OnDestroy()
    {
        _Dog.OnDogStateChange -= OnDogStateChanged;
    }

    void Update()
    {
        HandleInputs();

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
            case _Dog._DogState.ActionLock:
                break;
        }
    }

    void OnDogStateChanged(_Dog._DogState newState)
    {
        switch (newState)
        {
            case _Dog._DogState.Idle:
                OnIdleStart();
                break;
            case _Dog._DogState.Climbing:
                OnClimbStart();
                break;
            case _Dog._DogState.Urinating:
                OnUrinateStart();
                break;
            case _Dog._DogState.ActionLock:
                break;
        }
    }

    void LateUpdate()
    {
        _isGrounded = false;
    }

    void HandleInputs()
    {
        moveVector = new Vector3(Input.GetAxis(InputAxisHorizontal), 0f, Input.GetAxis(InputAxisVertical));

        if (Input.GetButton(InputButtonFire))
        {
            _Dog.DogState = _Dog._DogState.Urinating;
        }
        else if (Input.GetButtonUp(InputButtonFire))
        {
            _Dog.DogState = _Dog._DogState.Idle;
        }
        
    }

    void OnIdleStart()
    {
        Debug.Log("Start Dog Idle State");
    }

    void Idle()
    {
        // apply acceleration rates
        moveVector.x *= horizontalAccel;
        moveVector.y *= verticalAccel;

        // Apply the jumpInputModifier to the move Vector is not grouned
        moveVector *= _isGrounded ? idleInputModifier : jumpInputModifier;

        // Change move vector into object space
        moveVector = transform.TransformDirection(moveVector);

        // Add move vector to rigidbodies velocity as long as the velocity
        // is below the max velocity to apply input
        Vector2 frLimitied = new Vector2(rigidbody.velocity.x, rigidbody.velocity.z);
        if ((frLimitied + new Vector2(moveVector.x, moveVector.z)).magnitude < idleMaxForwardRightVel) {
            rigidbody.velocity += moveVector;
        }

        // Rotate the based off the mouse x input
        transform.Rotate(Vector3.up, Input.GetAxis(InputAxisRotateY) * cameraLookRate * Time.deltaTime);

        // Handle Jumping
        if (Input.GetButtonDown(InputButtonJump)) {
            Jump();
        }
    }

    void OnUrinateStart()
    {
        Debug.Log("Start Dog Urinate State");
    }

    void Urinate()
    {
        // apply acceleration rates
        moveVector.x *= horizontalAccel;
        moveVector.y *= verticalAccel;

        // Apply the jumpInputModifier to the move Vector is not grouned
        moveVector *= _isGrounded ? urinInputModifier : jumpInputModifier;

        // Change move vector into object space
        moveVector = transform.TransformDirection(moveVector);

        // Add move vector to rigidbodies velocity as long as the velocity
        // is below the max velocity to apply input
        Vector2 frLimitied = new Vector2(rigidbody.velocity.x, rigidbody.velocity.z);
        if ((frLimitied + new Vector2(moveVector.x, moveVector.z)).magnitude < idleMaxForwardRightVel)
        {
            rigidbody.velocity += moveVector;
        }

        // Rotate the based off the mouse x input
        transform.Rotate(Vector3.up, Input.GetAxis(InputAxisRotateY) *urinRotationModifier * cameraLookRate * Time.deltaTime);
    }

    void OnClimbStart()
    {
        Debug.Log("Start Dog Climb State");
    }  

    void Climb()
    {
        transform.position = Vector3.Lerp(transform.position, targetLocation, Time.deltaTime*5);
        if (Vector3.Distance(transform.position, targetLocation) < 0.1f || startTime < Time.time - 1)
        {
            _Dog.DogState=_Dog._DogState.Idle;
        }
    }

    void Jump()
    {
        RaycastHit spot = new RaycastHit();
        if (jumpController.CanJump(out spot))
        {
            startTime = Time.time;
            targetLocation = spot.point + new Vector3(0, 1, 0);
            _Dog.DogState = _Dog._DogState.Climbing;
        }
        else if (_isGrounded)
        {
            Vector3 jumpForceDir = Vector3.up;
            jumpForceDir += moveVector.normalized;
            jumpForceDir *= jumpForce;
            rigidbody.AddForce(jumpForceDir, jumpForceMode);

            Debug.DrawRay(transform.position, jumpForceDir, Color.red);
        }
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
                _isGrounded = true;
            }
        }
    }
}
