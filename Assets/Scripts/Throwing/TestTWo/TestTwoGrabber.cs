using UnityEngine;

public class TestTwoGrabber : MonoBehaviour
{
    public Transform holdPoint; // fixed position in front of camera
    private GameObject heldBall;

    void OnTriggerEnter(Collider other)
    {
        if (heldBall == null && other.CompareTag("Ball"))
        {
            GrabBall(other.gameObject);
        }
    }

    void GrabBall(GameObject ball)
    {
        heldBall = ball;
        Rigidbody rb = ball.GetComponent<Rigidbody>();
        rb.isKinematic = true;
        ball.transform.position = holdPoint.position;
        ball.transform.SetParent(holdPoint);
    }

    public void ReleaseBall(Vector3 velocity)
    {
        if (heldBall != null)
        {
            Rigidbody rb = heldBall.GetComponent<Rigidbody>();
            rb.isKinematic = false;
            rb.linearVelocity = velocity;
            heldBall.transform.SetParent(null);
            heldBall = null;
        }
    }
}

