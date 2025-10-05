using UnityEngine;

public class HandMouseControl : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private Transform handTarget;
    [SerializeField] private Transform elbowHint;
    [SerializeField] private Transform clavicle_r;

    [SerializeField] private float followSpeed = 10f;
    [SerializeField] private float minDistance = 1f;

    private float startingDepth;

    void Start()
    {
        // calculate how far the hand starts from the camera
        // so it stays at that depth during play
        Vector3 toHand = handTarget.position - cam.transform.position;
        startingDepth = Vector3.Dot(toHand, cam.transform.forward);
    }

    void Update()
    {
        // Ray from mouse into 3D space
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        // Use the starting depth to project the mouse into 3D
        Vector3 targetPos = ray.origin + ray.direction * startingDepth;

        // Clamp distance so arm doesn’t collapse
        Vector3 shoulderPos = clavicle_r.position;
        if (Vector3.Distance(targetPos, shoulderPos) < minDistance)
        {
            targetPos = shoulderPos + (targetPos - shoulderPos).normalized * minDistance;
        }

        // Smoothly move toward target
        handTarget.position = Vector3.Lerp(
            handTarget.position,
            targetPos,
            Time.deltaTime * followSpeed
        );
    }
}