using UnityEngine;

public class TestTwoChargeThrow : MonoBehaviour
{
    public TestTwoGrabber grabber;
    public Transform arm;
    public float maxRotation = 60f; // degrees back
    public float throwForce = 15f;
    private bool charging = false;
    private float currentRotation = 0f;

    void Update()
    {
        if (grabber == null) return;

        if (Input.GetMouseButtonDown(0) && grabber != null)
        {
            charging = true;
        }

        if (Input.GetMouseButtonUp(0) && charging)
        {
            Throw();
        }

        if (charging)
        {
            Charge();
        }
    }

    void Charge()
    {
        float rotateSpeed = 100f * Time.deltaTime;
        currentRotation = Mathf.Min(currentRotation + rotateSpeed, maxRotation);
        arm.localRotation = Quaternion.Euler(-currentRotation, 0, 0);
    }

    void Throw()
    {
        charging = false;
        // Snap arm forward
        arm.localRotation = Quaternion.identity;
        // Calculate forward throw
        Vector3 throwDir = arm.forward;
        grabber.ReleaseBall(throwDir * throwForce);
        currentRotation = 0f;
    }
}

