using UnityEngine;

public class TestTwoFluidThrow : MonoBehaviour
{
    public TestTwoGrabber grabber;
    public float throwMultiplier = 10f;

    private Vector3 swipeStart;

    void Update()
    {
        if (grabber == null) return;

        if (Input.GetMouseButtonDown(0))
        {
            swipeStart = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0) && grabberHasBall())
        {
            Vector3 swipeEnd = Input.mousePosition;
            Vector3 swipe = swipeEnd - swipeStart;

            // Convert swipe to world direction relative to camera
            Vector3 throwDir = Camera.main.transform.forward + Camera.main.transform.up * (swipe.y / Screen.height);
            throwDir.Normalize();

            grabber.ReleaseBall(throwDir * throwMultiplier);
        }
    }

    private bool grabberHasBall()
    {
        return true; // simple check; grabber only releases if holding a ball
    }
}

