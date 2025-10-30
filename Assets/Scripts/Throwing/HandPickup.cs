using UnityEngine;

public class HandPickup : MonoBehaviour
{
    [SerializeField] private Transform holdPoint; // Empty child where the ball will be held
    [SerializeField] private string ballTag = "Ball";
    [SerializeField] private Animator anim;
    private Rigidbody heldBall;
    private bool isHolding;

    private Vector3 targetPosition;
    private Quaternion targetRotation;

    void LateUpdate()
    {
        // cache the current hold point position (after Animator/RigBuilder)
        if ( isHolding && heldBall != null )
        {
            targetPosition = holdPoint.position;
            targetRotation = holdPoint.rotation;
        }
    }

    void FixedUpdate()
    {
        if ( isHolding && heldBall != null )
        {
            heldBall.MovePosition( targetPosition );
            heldBall.MoveRotation( targetRotation );
        }
    }

    void OnTriggerEnter( Collider other )
    {
        if ( !isHolding && other.CompareTag( ballTag ) )
        {
            Rigidbody rb = other.attachedRigidbody;
            if ( rb != null )
            {
                Pickup( rb );
            }
        }
    }

    private void Pickup( Rigidbody rb )
    {
        heldBall = rb;
        heldBall.useGravity = false;
        heldBall.linearVelocity = Vector3.zero;
        heldBall.angularVelocity = Vector3.zero;

        isHolding = true;

        if ( anim != null )
            anim.SetTrigger( "Grab" );
    }

    public bool IsHoldingBall()
    {
        return isHolding && heldBall != null;
    }

    public Rigidbody ReleaseBall()
    {
        if ( !isHolding || heldBall == null )
            return null;

        Rigidbody released = heldBall;

        heldBall.useGravity = true;
        heldBall = null;
        isHolding = false;

        if ( anim != null )
            anim.SetTrigger( "Release" );

        return released;
    }
}