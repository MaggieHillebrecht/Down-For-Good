using UnityEngine;

public class HandTransparencyController : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer handMesh;
    [SerializeField] private float fadeSpeed = 5f;

    private Material handMaterial;
    private float targetAlpha = 1f;

    void Start()
    {
        // Use material *instance* so we affect the live copy
        handMaterial = handMesh.material; 
    }

    void Update()
    {
        if (handMaterial == null) return;

        Color c = handMaterial.color;
        c.a = Mathf.Lerp(c.a, targetAlpha, Time.deltaTime * fadeSpeed);
        handMaterial.color = c;
    }

    public void SetVisible(bool visible)
    {
        targetAlpha = visible ? 1f : 0.1f; // slightly visible when faded
    }
}