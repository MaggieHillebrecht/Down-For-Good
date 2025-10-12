using UnityEngine;

public class BallThrowController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private HandPickup handPickup;
    [SerializeField] private Transform handTarget;
    [SerializeField] private Camera cam;
    [SerializeField] private Animator anim;
    [SerializeField] private TrajectoryLine trajectory;

    [Header("Throw Settings")]
    [SerializeField] private float minThrowForce = 5f;
    [SerializeField] private float maxThrowForce = 25f;
    [SerializeField] private float chargeSpeed = 10f;

    private float currentCharge = 0f;
    private bool isCharging = false;
    private Vector3 aimDirection;

    void Update()
    {
        if (!handPickup.IsHoldingBall()) return;

        // Start charging
        if (Input.GetMouseButtonDown(1))
        {
            isCharging = true;
            currentCharge = 0f;
            if (anim != null)
                anim.SetBool("Charging", true);
        }

        if (isCharging)
        {
            // Build charge
            currentCharge += Time.deltaTime * chargeSpeed;
            currentCharge = Mathf.Clamp01(currentCharge);

            // Update aim based on mouse
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                aimDirection = (hit.point - handTarget.position).normalized;
            }
            else
            {
                aimDirection = handTarget.forward;
            }

            // Show trajectory
            float throwPower = Mathf.Lerp(minThrowForce, maxThrowForce, currentCharge);
            Vector3 velocity = aimDirection * throwPower;
            trajectory.ShowTrajectory(handTarget.position, velocity);
        }

        // Release throw
        if (isCharging && Input.GetMouseButtonUp(1))
        {
            isCharging = false;
            if (anim != null)
            {
                anim.SetBool("Charging", false);
                anim.SetTrigger("Throw");
            }

            trajectory.Hide();
            ThrowBall();
        }
    }

    private void ThrowBall()
    {
        Rigidbody ball = handPickup.ReleaseBall();
        if (ball == null) return;

        float throwPower = Mathf.Lerp(minThrowForce, maxThrowForce, currentCharge);
        Vector3 throwVelocity = aimDirection * throwPower;

        ball.useGravity = true;
        ball.linearVelocity = throwVelocity;
    }
}