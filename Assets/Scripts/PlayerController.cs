using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour {
    [Header("Movement")]
    [SerializeField] private float speed = 10f;

    [Header("Camera")]
    [SerializeField] public Transform cameraRoot;
    [SerializeField] private float mouseSensitivity = 0.2f;
    [SerializeField] private float verticalClamp = 80f;

    private Rigidbody rb;
    private Vector2 movementVector;
    private Vector2 lookVector;
    private float verticalRotation = 0f;

    private void Awake() {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void OnMove(InputValue value) {
        movementVector = value.Get<Vector2>();
    }

    public void OnLook(InputValue value) {
        lookVector = value.Get<Vector2>();
    }

    private void FixedUpdate() {
        HandleMovement();
    }

    private void Update() {
        HandleLook();
    }

    private void HandleMovement() {
        Vector3 direction = GetCameraRelativeDirection();
        Vector3 newLinearVelocity = direction * speed;
        newLinearVelocity.y = rb.linearVelocity.y;
        rb.linearVelocity = newLinearVelocity;
    }

    private void HandleLook() {
        transform.Rotate(lookVector.x * mouseSensitivity * Vector3.up);

        verticalRotation -= lookVector.y * mouseSensitivity;
        verticalRotation = Mathf.Clamp(verticalRotation, -verticalClamp, verticalClamp);
        cameraRoot.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
    }

    private Vector3 GetCameraRelativeDirection() {
        Vector3 cameraForward = Vector3.ProjectOnPlane(cameraRoot.forward, Vector3.up).normalized;
        Vector3 cameraRight = Vector3.ProjectOnPlane(cameraRoot.right, Vector3.up).normalized;

        return cameraForward * movementVector.y + cameraRight * movementVector.x;
    }
}
