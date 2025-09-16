using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    public Camera cam;                 
    public float distanceFromCamera = 5f; 

    void Update()
    {
        Vector3 mousePos = Input.mousePosition;

        mousePos.x = Mathf.Clamp(mousePos.x, 0, Screen.width);
        mousePos.y = Mathf.Clamp(mousePos.y, 0, Screen.height);

        mousePos.z = distanceFromCamera;

        Vector3 worldPos = cam.ScreenToWorldPoint(mousePos);

        transform.position = worldPos;
    }
}
