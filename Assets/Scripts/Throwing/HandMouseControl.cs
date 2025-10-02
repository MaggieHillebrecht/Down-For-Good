using UnityEngine;

public class HandMouseControl : MonoBehaviour
{
    public Camera cam;
    public Transform handTarget;  
    public Transform elbowHint;   
    public Transform clavicle_r;  // assign clavicle_r bone
    public float followSpeed = 10f;
    public float planeHeight = 1.0f;
    public float minDistance = 0f; // prevents snapping

    void Update()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, new Vector3(0, planeHeight, 0));

        if (plane.Raycast(ray, out float enter))
        {
            Vector3 hit = ray.GetPoint(enter);

            // --- clamp distance so arm doesn't collapse ---
            Vector3 shoulderPos = clavicle_r.position;
            if (Vector3.Distance(hit, shoulderPos) < minDistance)
            {
                hit = shoulderPos + (hit - shoulderPos).normalized * minDistance;
            }

            // --- move the hand target smoothly ---
            handTarget.position = Vector3.Lerp(
                handTarget.position, 
                hit, 
                Time.deltaTime * followSpeed
            );

            // --- keep the elbow bent outward with a hint ---
            if (elbowHint != null)
            {
                Vector3 toHand = (handTarget.position - shoulderPos).normalized;
                Vector3 side = Vector3.Cross(Vector3.up, toHand); 
                elbowHint.position = shoulderPos + toHand * 0.5f + side * 0.3f; 
            }
        }
    }
}