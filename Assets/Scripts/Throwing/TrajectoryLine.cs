using UnityEngine;
using System.Collections.Generic;

public class TrajectoryLine : MonoBehaviour
{
    [SerializeField] private int linePoints = 30;
    [SerializeField] private float timeStep = 0.05f;
    [SerializeField] private LayerMask collisionMask;

    private LineRenderer line;

    void Awake()
    {
        line = GetComponent<LineRenderer>();
        line.positionCount = 0;
    }

    public void ShowTrajectory(Vector3 startPos, Vector3 initialVelocity)
    {
        List<Vector3> points = new List<Vector3>();
        Vector3 currentPos = startPos;
        Vector3 velocity = initialVelocity;

        for (int i = 0; i < linePoints; i++)
        {
            points.Add(currentPos);

            // simulate motion
            velocity += Physics.gravity * timeStep;
            Vector3 nextPos = currentPos + velocity * timeStep;

            // stop at collision
            if (Physics.Raycast(currentPos, nextPos - currentPos, out RaycastHit hit, (nextPos - currentPos).magnitude, collisionMask))
            {
                points.Add(hit.point);
                break;
            }

            currentPos = nextPos;
        }

        line.positionCount = points.Count;
        line.SetPositions(points.ToArray());
    }

    public void Hide()
    {
        line.positionCount = 0;
    }
}
