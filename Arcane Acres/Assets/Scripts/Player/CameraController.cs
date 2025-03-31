using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform lookAtTarget; // Reference to the player or camera target
    public float cameraRotationSpeed = 30f; // Speed of camera rotation around the player
    public float verticalRotationSpeed = 15f; // Speed of vertical camera rotation
    public float zoomSpeed = 5f;
    public float minZoom = 5f;
    public float maxZoom = 20f;
    public float inputSensitivity = 2f; // Factor to amplify the input
    public float rotationSmoothing = 5f; // Smoothing factor for camera rotation
    public Vector3 offset; // The initial offset from the player
    public LayerMask collisionLayers; // Layers to detect collision with

    private float currentVerticalAngle = 0f;
    private float currentHorizontalAngle = 0f;
    private Quaternion currentRotation;
    [SerializeField]
    private FarmingSimCharacterController characterController;

    void Start()
    {
        FindPlayer(); // Call the method to find the player at the start
        currentVerticalAngle = transform.eulerAngles.x;
        currentHorizontalAngle = transform.eulerAngles.y;
        currentRotation = transform.rotation; // Initialize the current rotation
    }

    void LateUpdate()
    {
        if (lookAtTarget != null && characterController != null && !characterController.isTargetLock)
        {
            HandleCameraRotation();
            HandleVerticalRotation();
        }
        HandleCameraZoom();
        UpdateCameraPosition();
        PreventCameraCollision();
    }

    // Method to find the player dynamically
    void FindPlayer()
    {
        GameObject playerObject = GameObject.FindWithTag("Player");
        if (playerObject != null)
        {
            lookAtTarget = playerObject.transform;
            characterController = playerObject.GetComponent<FarmingSimCharacterController>();
        }
        else
        {
            Debug.LogWarning("Player not found. Make sure the player has the 'Player' tag.");
        }
    }

    void HandleCameraRotation()
    {
        float horizontalInput = Input.GetAxis("Mouse X") * cameraRotationSpeed * inputSensitivity;
        currentHorizontalAngle += horizontalInput;
    }

    void HandleVerticalRotation()
    {
        float verticalInput = Input.GetAxis("Mouse Y") * verticalRotationSpeed * inputSensitivity;
        currentVerticalAngle += verticalInput;
        currentVerticalAngle = Mathf.Clamp(currentVerticalAngle, 30f, 60f); // Clamp vertical angle to prevent extreme tilting
    }

    void HandleCameraZoom()
    {
        float scrollInput = Input.GetAxis("Mouse ScrollWheel") * inputSensitivity;
        offset.y -= scrollInput * zoomSpeed;
        offset.y = Mathf.Clamp(offset.y, minZoom, maxZoom);
    }

    void UpdateCameraPosition()
    {
        if (lookAtTarget == null) return;

        // Calculate the target rotation based on the current angles
        Quaternion targetRotation = Quaternion.Euler(currentVerticalAngle, currentHorizontalAngle, 0f);

        // Smoothly interpolate the camera's rotation towards the target rotation
        currentRotation = Quaternion.Slerp(currentRotation, targetRotation, Time.deltaTime * rotationSmoothing);

        // Update the camera's position based on the smoothed rotation
        transform.position = lookAtTarget.position + currentRotation * offset;

        // Ensure the camera is looking at the player
        transform.LookAt(lookAtTarget.position);
    }

    void PreventCameraCollision()
    {
        RaycastHit hit;

        // Raycast from the player towards the camera
        if (lookAtTarget != null && Physics.Raycast(lookAtTarget.position, transform.position - lookAtTarget.position, out hit, offset.magnitude, collisionLayers))
        {
            // Adjust the camera's position to the hit point
            transform.position = hit.point;
        }
    }
}
