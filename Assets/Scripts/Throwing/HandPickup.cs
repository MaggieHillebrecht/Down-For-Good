using UnityEngine;

public class HandPickup : MonoBehaviour
{
    [SerializeField] private Transform holdPoint; // Empty child where the ball will be held
    [SerializeField] private string ballTag = "Ball";
    [SerializeField] private Animator anim;
    [SerializeField] private float pickupRadius = 0.5f;

    private Rigidbody heldBall;
    private bool isHolding;

    private Vector3 targetPosition;
    private Quaternion targetRotation;

    void LateUpdate()
    {
        // Cache the current hold point position (after Animator/RigBuilder)
        if (isHolding && heldBall != null)
        {
            targetPosition = holdPoint.position;
            targetRotation = holdPoint.rotation;
        }

        if (isHolding && Input.GetMouseButtonDown(0))
        {
            Drop();
        }
    }

    void FixedUpdate()
    {
        // Move the held object via physics in sync with the last known hand position
        if (isHolding && heldBall != null)
        {
            heldBall.MovePosition(targetPosition);
            heldBall.MoveRotation(targetRotation);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (!isHolding && other.CompareTag(ballTag))
        {
            Rigidbody rb = other.attachedRigidbody;
            if (rb != null)
            {
                Pickup(rb);
            }
        }
    }

    private void Pickup(Rigidbody rb)
    {
        heldBall = rb;
        heldBall.useGravity = false;
        heldBall.linearVelocity = Vector3.zero;
        heldBall.angularVelocity = Vector3.zero;

        isHolding = true;

        if (anim != null)
            anim.SetTrigger("Grab");
    }

    private void Drop()
    {
        if (heldBall != null)
        {
            heldBall.useGravity = true;
            heldBall.linearDamping = 0f;
            heldBall.angularDamping = 0.05f;
            heldBall = null;
        }

        isHolding = false;
        if (anim != null)
            anim.SetTrigger("Release");
    }
}