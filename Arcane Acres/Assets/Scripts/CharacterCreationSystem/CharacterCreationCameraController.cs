using UnityEngine;
using UnityEngine.EventSystems;

public class CharacterCreationCameraController : MonoBehaviour
{
    public Transform character;           // Reference to the character transform
    public Camera characterCamera;        // Reference to the camera
    public float rotationSpeed = 100f;    // Speed of rotation
    public float zoomSpeed = 2f;          // Speed of zoom
    public float minZoomDistance = 2f;    // Minimum zoom distance
    public float maxZoomDistance = 10f;   // Maximum zoom distance
    public Transform zoomTarget;          // The spot to center the zoom on, set in the inspector

    private Vector3 initialCameraOffset;  // Initial offset of the camera from the zoom target

    void Start()
    {
        // Store the initial camera offset based on the zoom target
        if (characterCamera != null && zoomTarget != null)
        {
            initialCameraOffset = characterCamera.transform.position - zoomTarget.position;
        }
    }

    void Update()
    {
        // Check if the mouse is over UI
        if (IsPointerOverUI())
            return;  // Don't allow control when hovering over UI

        // Lock the cursor when the mouse button is pressed, and unlock it when released
        if (Input.GetMouseButtonDown(0) && !IsPointerOverUI())
        {
            LockCursor();
        }

        if (Input.GetMouseButtonUp(0))
        {
            UnlockCursor();
        }

        // Handle character rotation
        if (Input.GetMouseButton(0))  // Left mouse button for rotating
        {
            RotateCharacter();
        }

        // Handle zooming in/out with the mouse scroll wheel
        HandleZoom();
    }

    void RotateCharacter()
    {
        float mouseX = Input.GetAxis("Mouse X");

        // Rotate the character only around the Y axis
        character.Rotate(Vector3.up, -mouseX * rotationSpeed * Time.deltaTime);
    }

    void HandleZoom()
    {
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");

        // Calculate the new zoom distance
        Vector3 cameraPosition = characterCamera.transform.position;
        Vector3 direction = (cameraPosition - zoomTarget.position).normalized;

        float distance = Vector3.Distance(cameraPosition, zoomTarget.position);
        float newDistance = Mathf.Clamp(distance - scrollInput * zoomSpeed, minZoomDistance, maxZoomDistance);

        // Apply the zoom, moving the camera toward or away from the zoom target
        characterCamera.transform.position = zoomTarget.position + direction * newDistance;
    }

    bool IsPointerOverUI()
    {
        // Detect if the pointer is over any UI element
        return EventSystem.current.IsPointerOverGameObject();
    }

    void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;  // Lock the cursor to the center of the screen
        Cursor.visible = false;                   // Hide the cursor
    }

    void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;   // Unlock the cursor
        Cursor.visible = true;                    // Show the cursor
    }
}
