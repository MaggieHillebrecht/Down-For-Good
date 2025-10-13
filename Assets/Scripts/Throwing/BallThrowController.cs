using UnityEngine;

public class BallThrowController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private HandPickup handPickup;
    [SerializeField] private Transform handTarget;
    [SerializeField] private Camera cam;
    [SerializeField] private Animator anim;
    [SerializeField] private CrosshairController crosshair;
    [SerializeField] private HandTransparencyController handTransparency;

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

        // Start charging (right mouse down)
        if (Input.GetMouseButtonDown(1))
        {
            isCharging = true;
            currentCharge = 0f;

            if (anim != null)
                anim.SetBool("Charging", true);

            if (handTransparency != null)
                handTransparency.SetVisible(false);

            if (crosshair != null)
                crosshair.SetVisible(true); // show crosshair when charging
        }

        // While charging (holding right click)
        if (isCharging && Input.GetMouseButton(1))
        {
            currentCharge += Time.deltaTime * chargeSpeed;
            currentCharge = Mathf.Clamp01(currentCharge);

            // Raycast to determine aim target
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                aimDirection = (hit.point - handTarget.position).normalized;
                crosshair.UpdatePosition(hit.point);
            }
            else
            {
                aimDirection = handTarget.forward;
                crosshair.UpdatePosition(handTarget.position + aimDirection * 10f);
            }
        }

        // Release throw (right mouse up)
        if (isCharging && Input.GetMouseButtonUp(1))
        {
            isCharging = false;

            if (anim != null)
            {
                anim.SetBool("Charging", false);
                anim.SetTrigger("Throw");
            }

            if (handTransparency != null)
                handTransparency.SetVisible(true);

            if (crosshair != null)
                crosshair.SetVisible(false); // hide crosshair after throw

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