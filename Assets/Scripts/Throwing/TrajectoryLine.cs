using UnityEngine;

public class TrajectoryLine : MonoBehaviour
{
    [Header("Trajectory Settings")]
    [SerializeField] private int steps = 20;
    [SerializeField] private float stepTime = 0.1f;
    [SerializeField] private Color lineColor = Color.yellow;
    [SerializeField] private float lineWidth = 0.02f;

    private LineRenderer lineRenderer;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Unlit/Color"));
        lineRenderer.material.color = lineColor;
        lineRenderer.widthMultiplier = lineWidth;
        lineRenderer.positionCount = 0;
    }

    public void DrawTrajectory(Vector3 startPosition, Vector3 direction, float force)
    {
        lineRenderer.positionCount = steps;

        Vector3 startVel = direction * force;
        Vector3 gravity = Physics.gravity;

        for (int i = 0; i < steps; i++)
        {
            float t = stepTime * i;
            Vector3 pos = startPosition + (startVel * t) + (0.5f * gravity * t * t);
            lineRenderer.SetPosition(i, pos);
        }
    }

    public void ClearTrajectory()
    {
        if (lineRenderer)
            lineRenderer.positionCount = 0;
    }
}
