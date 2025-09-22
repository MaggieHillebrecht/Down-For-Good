using UnityEngine;

public class HandPickupAndRelease : MonoBehaviour
{
    public float throwForceMultiplier = 1.5f; // Scales hand velocity into throw force
    private GameObject heldObject;
    private MouseMovement handMovement;       // Reference to your MouseMovement script

    void Start()
    {
        handMovement = GetComponent<MouseMovement>();
        if (handMovement == null)
            Debug.LogError("HandPickupAndRelease requires MouseMovement on the same GameObject!");
    }

    void Update()
    {
        // Drop held object with left mouse release
        if (heldObject != null && Input.GetMouseButtonUp(0))
        {
            DropObject();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball") && heldObject == null && Input.GetMouseButton(0))
        {
            PickupObject(other.gameObject);
        }
    }

    private void PickupObject(GameObject obj)
    {
        heldObject = obj;
        heldObject.transform.SetParent(transform);
        heldObject.transform.localPosition = Vector3.zero;

        Rigidbody rb = heldObject.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }

    private void DropObject()
    {
        if (heldObject == null) return;

        Rigidbody rb = heldObject.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = false;
            heldObject.transform.SetParent(null);

            // Apply throw using hand velocity from MouseMovement
            rb.AddForce(handMovement.handVelocity * throwForceMultiplier, ForceMode.VelocityChange);
        }

        heldObject = null;
    }
}