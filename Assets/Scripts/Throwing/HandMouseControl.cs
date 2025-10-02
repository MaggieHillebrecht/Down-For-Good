using UnityEngine;

public class HandMouseControl : MonoBehaviour
{
    public Camera cam;
    public Transform handTarget;
    public Transform elbowHint;
    public Transform clavicle_r;
    public float followSpeed = 10f;
    public float minDistance = 0.3f;
    public float depth = 2.0f;
    public float depthSpeed = 2f;
    public bool isRightArm = true; // toggle if elbow bends wrong way

    void Update()
    {
        // scroll wheel for depth
        depth += Input.GetAxis("Mouse ScrollWheel") * depthSpeed;

        // 3D target from mouse + depth
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        Vector3 targetPos = ray.origin + ray.direction * depth;

        // clamp so it doesn’t collapse into the shoulder
        Vector3 shoulderPos = clavicle_r.position;
        if (Vector3.Distance(targetPos, shoulderPos) < minDistance)
        {
            targetPos = shoulderPos + (targetPos - shoulderPos).normalized * minDistance;
        }

        // follows the mouse and is in the direction of the target
        handTarget.position = Vector3.Lerp(
            handTarget.position,
            targetPos,
            Time.deltaTime * followSpeed
        );

        // elbow hint placement
        if (elbowHint != null)
        {
            Vector3 toHand = (handTarget.position - shoulderPos).normalized;
            Vector3 side = cam.transform.right;

            if (isRightArm) side = -side;

            elbowHint.position = shoulderPos + toHand * 0.5f + side * 0.3f;
        }
    }
}