using UnityEngine;

public class FarmingSimCharacterController : MonoBehaviour
{
    public float walkSpeed = 2f;
    public float runSpeed = 4f;
    public float blendSpeed = 5f; // Speed at which to blend between animations
    public float gravity = -9.81f; // Gravity value
    public float groundCheckDistance = 0.2f; // Distance to check if the player is on the ground
    public LayerMask groundLayer; // Layer mask to identify what counts as ground
    public float playerRotationSpeed = 15f; // Speed of player rotation on the Y-axis

    private Animator animator;
    private CharacterController controller;
    private Vector3 moveDirection;
    private float currentAnimatorSpeed;
    private bool isGrounded;
    private Vector3 velocity;
    public bool isTargetLock;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        currentAnimatorSpeed = 0f;
    }

    void Update()
    {
        HandleMovement();
        HandleGravity();
        UpdateAnimator();
    }

    void HandleMovement()
    {
        isTargetLock = Input.GetButton("TargetLock");

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if (isTargetLock)
        {
            // When TargetLock is held, keep the player facing forward while allowing left and right movement
            moveDirection = transform.right * horizontal + transform.forward * vertical;
        }
        else
        {
            Vector3 forward = Camera.main.transform.forward;
            Vector3 right = Camera.main.transform.right;

            forward.y = 0f;
            right.y = 0f;

            forward.Normalize();
            right.Normalize();

            moveDirection = forward * vertical + right * horizontal;
        }

        // Normalize the movement direction to prevent diagonal speed increase
        if (moveDirection.magnitude > 1f)
        {
            moveDirection.Normalize();
        }

        float speed = Input.GetButton("Run") ? runSpeed : walkSpeed;
        moveDirection *= speed;

        // Handle very small movement values (due to floating-point precision issues)
        if (moveDirection.magnitude < 0.01f)
        {
            moveDirection = Vector3.zero;
        }

        controller.Move(moveDirection * Time.deltaTime);

        // If the player is moving, rotate towards the movement direction
        if (!isTargetLock && moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, playerRotationSpeed * Time.deltaTime);
        }
    }

    void HandleGravity()
    {
        // Check if the player is grounded
        isGrounded = Physics.CheckSphere(transform.position, groundCheckDistance, groundLayer);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Small negative value to keep the player grounded
        }

        // Apply gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    void UpdateAnimator()
    {
        float targetSpeed = 0f;
        if (moveDirection.magnitude > 0.01f)
        {
            targetSpeed = Input.GetButton("Run") ? 2f : 1f;
        }

        // Smoothly blend to the target speed value
        currentAnimatorSpeed = Mathf.Lerp(currentAnimatorSpeed, targetSpeed, blendSpeed * Time.deltaTime);

        // Ensure we zero out the animator speed when it gets close enough to 0 to avoid flipping values
        if (Mathf.Abs(currentAnimatorSpeed) < 0.01f)
        {
            currentAnimatorSpeed = 0f;
        }

        animator.SetFloat("Speed", currentAnimatorSpeed);
    }
}
