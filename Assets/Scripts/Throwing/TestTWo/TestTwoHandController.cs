using UnityEngine;

public class TestTwoHandController : MonoBehaviour
{
    public Camera cam; // assign your main camera
    public float moveSpeed = 10f; // smooth movement speed
    public LayerMask moveableLayers; // which layers hand can hover over

    private Vector3 targetPos;

    void Update()
    {
        MoveHandWithMouse();
    }

    void MoveHandWithMouse()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 100f, moveableLayers))
        {
            targetPos = hit.point;
            transform.position = Vector3.Lerp(transform.position, targetPos, moveSpeed * Time.deltaTime);
        }
    }
}
