using UnityEngine;

public class TestTwo : MonoBehaviour
{
    private Rigidbody rb;
    private Camera cam;

    private bool isHeld = false;
    private Vector3 mouseStartPos;
    private Vector3 mouseEndPos;

    [Header("Toss Settings")]
    public float tossForce = 10f;
    public float sideForce = 4f;   // horizontal influence
    public float upForce = 3f;     // vertical arc
    public float smoothMoveSpeed = 10f; // how fast it moves to hand position

    private Vector3 targetHoldPosition;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cam = Camera.main;
    }

    void OnMouseDown()
    {
        isHeld = true;
        rb.isKinematic = true;

        // bottom center screen (20% up from bottom)
        Vector3 screenPos = new Vector3(Screen.width / 2, Screen.height * 0.2f, cam.nearClipPlane + 2f);
        targetHoldPosition = cam.ScreenToWorldPoint(screenPos);

        mouseStartPos = Input.mousePosition;
    }

    void Update()
    {
        if (isHeld)
        {
            // Smoothly move to hand position
            transform.position = Vector3.Lerp(transform.position, targetHoldPosition, Time.deltaTime * smoothMoveSpeed);
        }
    }

    void OnMouseUp()
    {
        if (!isHeld) return;

        isHeld = false;
        rb.isKinematic = false;

        mouseEndPos = Input.mousePosition;
        Vector3 dragVector = mouseEndPos - mouseStartPos;

        // Forward base throw
        Vector3 throwDirection = cam.transform.forward;

        // Side adjustment (horizontal swipe)
        throwDirection += cam.transform.right * (dragVector.x / Screen.width) * sideForce;

        // Up adjustment (vertical swipe)
        throwDirection += cam.transform.up * (dragVector.y / Screen.height) * upForce;

        // Swipe strength (clamped for control)
        float strength = Mathf.Clamp(dragVector.magnitude / Screen.height, 0.1f, 1.5f);

        // Final force
        rb.AddForce(throwDirection.normalized * strength * tossForce, ForceMode.Impulse);
    }

}
