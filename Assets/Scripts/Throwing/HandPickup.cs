using UnityEngine;

public class HandPickup : MonoBehaviour
{
    [SerializeField] private Transform holdPoint;     // Empty child where the ball will be held
    [SerializeField] private string ballTag = "Ball";
    [SerializeField] private Animator anim;           // Drag your Animator here in the Inspector

    [SerializeField] private float pickupRadius = 0.5f;

    private Rigidbody heldBall;
    private bool isHolding;

    void OnTriggerEnter(Collider other)
    {
        // Try to pick up if we’re not already holding something
        if (!isHolding && other.CompareTag(ballTag))
        {
            Rigidbody rb = other.attachedRigidbody;
            if (rb != null)
            {
                Pickup(rb);
            }
        }
    }

    void Update()
    {
        // Keep the ball following the hold point
        if (isHolding && heldBall != null)
        {
            heldBall.MovePosition(holdPoint.position);
        }

        // Right-click to drop (for testing)
        if (isHolding && Input.GetMouseButtonDown(1))
        {
            Drop();
        }
    }

    private void Pickup(Rigidbody rb)
    {
        heldBall = rb;
        heldBall.useGravity = false;
        heldBall.linearVelocity = Vector3.zero;
        heldBall.angularVelocity = Vector3.zero;
        heldBall.transform.position = holdPoint.position;

        // Optional: parent the ball (to follow rotation)
        heldBall.transform.SetParent(holdPoint, true);

        isHolding = true;

        // Play grab animation
        if (anim != null)
        {
            anim.SetTrigger("Grab");
        }
    }

    private void Drop()
    {
        if (heldBall != null)
        {
            heldBall.useGravity = true;
            heldBall.transform.SetParent(null);
            heldBall = null;
        }

        isHolding = false;
    }
}