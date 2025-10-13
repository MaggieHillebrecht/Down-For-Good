using UnityEngine;
using UnityEngine.UI;

public class CrosshairController : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private Image crosshairImage;
    [SerializeField] private float smoothSpeed = 15f;

    private RectTransform rectTransform;
    private bool isVisible = false;
    private Vector3 targetScreenPos;

    void Awake()
    {
        rectTransform = crosshairImage.GetComponent<RectTransform>();
        crosshairImage.enabled = false;
    }

    public void SetVisible(bool visible)
    {
        isVisible = visible;
        crosshairImage.enabled = visible;
    }

    public void UpdatePosition(Vector3 worldTarget)
    {
        if (!isVisible) return;

        // Convert world target to screen space
        targetScreenPos = cam.WorldToScreenPoint(worldTarget);

        // Smooth follow movement
        rectTransform.position = Vector3.Lerp(
            rectTransform.position,
            targetScreenPos,
            Time.deltaTime * smoothSpeed
        );
    }
}
