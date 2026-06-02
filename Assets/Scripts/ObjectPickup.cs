using UnityEngine;

public class ObjectPickup : MonoBehaviour {
    [Header("Pickup Settings")]
    [SerializeField] private Transform cameraRoot;
    [SerializeField] private float holdDistance = 8f;
    [SerializeField] private float pickupRange = 8f;
    [SerializeField] private LayerMask pickupLayer;

    private GameObject heldObject;
    private float originalDistance;
    private Vector3 originalScale;
    private Vector3 holdOffset;

    private void Update() {
        if (heldObject != null) HoldObject();
    }

    private void HoldObject() {
        heldObject.transform.position = cameraRoot.position + cameraRoot.forward * holdDistance + holdOffset;
    }

    public void TryInteract() {
        if (heldObject == null) TryPickup();
        else DropObject();
    }

    private void TryPickup() {
        Ray ray = new Ray(cameraRoot.position, cameraRoot.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, pickupRange, pickupLayer)) {
            heldObject = hit.collider.gameObject;
            originalDistance = hit.distance;
            originalScale = heldObject.transform.localScale;
            holdOffset = heldObject.transform.position - (cameraRoot.position + cameraRoot.forward * holdDistance);

            Rigidbody heldObjectRb = heldObject.GetComponent<Rigidbody>();
            if (heldObjectRb != null) heldObjectRb.isKinematic = true;
        }
    }

    private void DropObject() {
        float scaleFactor = holdDistance / originalDistance;
        heldObject.transform.localScale = originalScale * scaleFactor;

        Rigidbody heldObjectRb = heldObject.GetComponent<Rigidbody>();
        if (heldObjectRb != null) heldObjectRb.isKinematic = false;
        heldObject = null;
    }
}
