using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlatformerPlayerController : MonoBehaviour {
    [Header("Movement")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 7f;

    private Rigidbody rb;
    private float movement;
    private bool jumpRequested;
    private bool isGrounded;

    private void Awake() {
        rb = GetComponent<Rigidbody>();
    }

    public void OnMove(InputValue value) {
        movement = value.Get<Vector2>().x;
    }

    public void OnJump(InputValue value) {
        if (value.isPressed && isGrounded) {
            jumpRequested = true;
        }
    }

    private void FixedUpdate() {
        HandleMovement();
        HandleJump();
    }

    private void HandleMovement() {
        Vector3 velocity = rb.linearVelocity;
        velocity.x = movement * speed;
        rb.linearVelocity = velocity;
    }

    private void HandleJump() {
        if (!jumpRequested) return;

        jumpRequested = false;

        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    private void OnCollisionStay(Collision collision) {
        isGrounded = true;
    }

    private void OnCollisionExit(Collision collision) {
        isGrounded = false;
    }
}
