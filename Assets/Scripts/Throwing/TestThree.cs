using UnityEngine;

public class TestThree : MonoBehaviour
{
    [Header("Arm Joints")]
    public Transform upperArm;
    public Transform forearm;
    public Transform hand;

    [Header("Settings")]
    public float rotationSpeed = 60f;      // Degrees per second
    public Transform holdPoint;
    public float throwMultiplier = 2f;
    public float baseThrowForce = 5f;

    private GameObject heldBall = null;
    private bool sPressed = false;
    private Vector3 lastHandPosition;

    void Start()
    {
        lastHandPosition = hand.position;
    }

    void Update()
    {
        HandleJointMovement();
        HandlePickUp();
        HandleThrowCombo();
        lastHandPosition = hand.position;
    }

    void HandleJointMovement()
    {
        // Upper arm rotation
        if (Input.GetKey(KeyCode.W)) upperArm.Rotate(Vector3.right, rotationSpeed * Time.deltaTime);
        if (Input.GetKey(KeyCode.S)) upperArm.Rotate(Vector3.right, -rotationSpeed * Time.deltaTime);

        // Forearm rotation
        if (Input.GetKey(KeyCode.A)) forearm.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
        if (Input.GetKey(KeyCode.D)) forearm.Rotate(Vector3.forward, -rotationSpeed * Time.deltaTime);
    }

    void HandlePickUp()
    {
        if (heldBall != null)
        {
            heldBall.transform.position = holdPoint.position;
        }
    }

    void HandleThrowCombo()
    {
        if (heldBall == null) return;

        if (Input.GetKeyDown(KeyCode.S))
        {
            sPressed = true;
            Invoke("ResetCombo", 0.5f); // short window to press W
        }

        if (sPressed && Input.GetKeyDown(KeyCode.W))
        {
            ThrowBall();
            sPressed = false;
            CancelInvoke("ResetCombo");
        }
    }

    void ResetCombo()
    {
        sPressed = false;
    }

    void ThrowBall()
    {
        if (heldBall == null) return;

        Rigidbody rb = heldBall.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = false;

            // Calculate hand velocity
            Vector3 handVelocity = (hand.position - lastHandPosition) / Time.deltaTime;

            // Combine velocity with base force
            Vector3 throwForce = handVelocity * throwMultiplier + hand.forward * baseThrowForce;

            rb.AddForce(throwForce, ForceMode.VelocityChange);
        }

        heldBall = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (heldBall == null && other.CompareTag("Ball"))
        {
            heldBall = other.gameObject;
            Rigidbody rb = heldBall.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = true;
            }
        }
    }
}