using UnityEngine;

[RequireComponent(typeof(Collider))]
public class HandPickup : MonoBehaviour
{
    [SerializeField] private Transform holdPoint;  // Empty child where the ball will be held
    [SerializeField] private float pickupRadius = 0.5f;
    [SerializeField] private string ballTag = "Ball";

    private Rigidbody heldBall;
    private bool isHolding;
    private Animator animation;

    private void Start()
    {
        animation = GetComponent<Animator>();
    }

    void OnTriggerEnter(Collider other)
    {
        // Pick up if touching a ball and not already holding one
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
        // Keep the held ball following the holdPoint
        if (isHolding && heldBall != null)
        {
            heldBall.MovePosition(holdPoint.position);
        }

        // Drop on mouse click (temporary test)
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

        isHolding = true;
        animation.SetBool("Grabbing", isHolding);
    }

    private void Drop()
    {
        heldBall.useGravity = true;
        heldBall = null;
        isHolding = false;
        animation.SetBool("Grabbing", isHolding);
    }
}
