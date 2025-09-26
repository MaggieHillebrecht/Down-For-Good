using UnityEngine;

public class StretchyArm : MonoBehaviour
{
    public Transform shoulder; // fixed base
    public Transform hand;     // moving hand
    public float armRadius = 0.1f; // radius of the cylinder (optional)

    private Vector3 initialScale;

    void Start()
    {
        initialScale = transform.localScale;
    }

    void LateUpdate()
    {
        UpdateArm();
    }

    void UpdateArm()
    {
        Vector3 direction = hand.position - shoulder.position;
        float distance = direction.magnitude;

        // Position arm halfway between shoulder and hand
        transform.position = shoulder.position + direction * 0.5f;

        // Scale arm along its local Y axis to match distance
        Vector3 scale = initialScale;
        scale.y = distance * 0.5f; // assuming your cylinder’s height = 2 units
        transform.localScale = scale;

        // Rotate arm to point toward hand
        transform.rotation = Quaternion.LookRotation(direction);
        transform.Rotate(90, 0, 0); // rotate cylinder so Y axis points along the vector
    }
}
