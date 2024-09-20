using System;
using UnityEngine;

public class FarmingSimCharacterController : MonoBehaviour
{

    [Header("References")]
    [Tooltip("Reference to player's animator.")]
    private Animator animator;
    [Tooltip("Reference to player's character controller.")]
    private CharacterController controller;

    [Header("Variables")]
    [Tooltip("How fast the player walks.")]
    public float walkSpeed = 2f;
    [Tooltip("How fast the player runs.")]
    public float runSpeed = 4f;
    [Tooltip("Speed of player rotation on the Y-axis.")]
    public float playerRotationSpeed = 15f;
    [Tooltip("How high the player can jump.")]
    public float jumpHeight = 5;

    [Header("Layer Masks")]
    [Tooltip("Layer mask to identify what counts as ground.")]
    public LayerMask[] groundLayers;

    [Header("Bools")]
    [SerializeField]
    [Tooltip("Is the player in contact with the ground layer?")]
    private bool isGrounded;
    [Tooltip("Is the player locked on to a target?")]
    public bool isTargetLock;

    // Private variables
    private float currentAnimatorSpeed;

    // Vector3s
    private Vector3 moveDirection;
    private Vector3 velocity;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        currentAnimatorSpeed = 0f;
    }

    void Update()
    {
        HandleMovement();
        HandleJump();
        HandleGravity();
        UpdateAnimator();
    }

    private void HandleJump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            animator.SetTrigger("Jump");
        }
    }

    public void Jump()
    {
        if (isGrounded)
        {
            // Calculate jump velocity based on jump height and gravity
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y);
        }
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
        foreach(LayerMask layermask in groundLayers)
        {
            if(Physics.CheckSphere(transform.position, 0.3f, layermask))
            {
                isGrounded = true;
            }
            else
            {
                isGrounded = false;
            }
        
        }
        animator.SetBool("isGrounded", isGrounded);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Small negative value to keep the player grounded
        }

        // Apply gravity
        velocity.y += Physics.gravity.y * Time.deltaTime;
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
        currentAnimatorSpeed = Mathf.Lerp(currentAnimatorSpeed, targetSpeed, 5 * Time.deltaTime);

        // Ensure we zero out the animator speed when it gets close enough to 0 to avoid flipping values
        if (Mathf.Abs(currentAnimatorSpeed) < 0.01f)
        {
            currentAnimatorSpeed = 0f;
        }

        animator.SetFloat("Speed", currentAnimatorSpeed);
    }
}
