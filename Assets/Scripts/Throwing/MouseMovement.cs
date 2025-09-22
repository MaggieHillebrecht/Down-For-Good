using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    public Camera cam;
    public float sensitivity = 0.5f;   // Smooth movement
    public float scrollSpeed = 2f;     // Scroll wheel moves forward/back
    public float maxForward = -4f;     // Max forward Z
    public float minBackward = -7f;   // Optional min backward Z

    [HideInInspector] public Vector3 handVelocity; // <-- Added

    private float currentZ;
    private Vector3 lastHandPos;

    void Start()
    {
        currentZ = transform.position.z;
        lastHandPos = transform.position;
    }

    void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        // Scroll wheel forward/back
        float scrollInput = Input.mouseScrollDelta.y;
        currentZ += scrollInput * scrollSpeed;
        currentZ = Mathf.Min(currentZ, maxForward);
        currentZ = Mathf.Max(currentZ, minBackward);

        // Mouse X/Y movement
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        Plane movementPlane = new Plane(Vector3.forward, new Vector3(0, 0, currentZ));

        if (movementPlane.Raycast(ray, out float enter))
        {
            Vector3 targetPos = ray.GetPoint(enter);

            // Smooth movement
            transform.position = Vector3.Lerp(transform.position, targetPos, sensitivity);
        }

        // Calculate hand velocity for throwing
        handVelocity = (transform.position - lastHandPos) / Time.deltaTime;
        lastHandPos = transform.position;
    }
}
