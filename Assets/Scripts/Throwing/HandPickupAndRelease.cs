using UnityEngine;

public class HandPickupAndRelease : MonoBehaviour
{
      public float shakeThreshold = 1000f; // how fast you need to move the mouse to release
    public float throwForce = 10f;       // force applied when throwing

    private GameObject heldObject;
    private Vector3 lastMousePos;

    void Start()
    {
        lastMousePos = Input.mousePosition;
    }

    void Update()
    {
        // Track mouse velocity
        Vector3 mouseDelta = Input.mousePosition - lastMousePos;
        float mouseSpeed = mouseDelta.magnitude / Time.deltaTime;
        lastMousePos = Input.mousePosition;

        // Drop if shaking fast
        if (heldObject != null && mouseSpeed > shakeThreshold)
        {
            DropBall(mouseDelta.normalized);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball") && heldObject == null)
        {
            PickupBall(other.gameObject);
        }
    }

    void PickupBall(GameObject ball)
    {
        heldObject = ball;
        heldObject.transform.SetParent(transform);
        heldObject.transform.localPosition = Vector3.zero;
        Rigidbody rb = heldObject.GetComponent<Rigidbody>();
        if (rb != null) rb.isKinematic = true;
    }

    void DropBall(Vector3 throwDirection)
    {
        Rigidbody rb = heldObject.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = false;
            heldObject.transform.SetParent(null);
            rb.AddForce(throwDirection * throwForce, ForceMode.VelocityChange);
        }
        heldObject = null;
    }
}
