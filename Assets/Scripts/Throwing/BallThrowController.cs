using UnityEngine;

public class BallThrowController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private HandPickup handPickup;
    [SerializeField] private Transform handTarget;
    [SerializeField] private Animator anim;

    [Header("Throw Settings")]
    [SerializeField] private float minThrowForce = 5f;
    [SerializeField] private float maxThrowForce = 25f;
    [SerializeField] private float chargeSpeed = 10f;

    private float currentCharge = 0f;
    private bool isCharging = false;

    void Update()
    {
        if (!handPickup.IsHoldingBall()) return;

        // Start charging on right mouse down
        if (Input.GetMouseButtonDown(1))
        {
            isCharging = true;
            currentCharge = 0f;
            if (anim != null)
                anim.SetBool("Charging", true);
        }

        // While holding right mouse button
        if (isCharging && Input.GetMouseButton(1))
        {
            currentCharge += Time.deltaTime * chargeSpeed;
            currentCharge = Mathf.Clamp01(currentCharge);
        }

        // Release and throw
        if (isCharging && Input.GetMouseButtonUp(1))
        {
            isCharging = false;
            if (anim != null)
            {
                anim.SetBool("Charging", false);
                anim.SetTrigger("Throw");
            }

            ThrowBall();
        }
    }

    private void ThrowBall()
    {
        Rigidbody ball = handPickup.ReleaseBall(); // release from hand
        if (ball == null) return;

        float throwPower = Mathf.Lerp(minThrowForce, maxThrowForce, currentCharge);

        // Use hand forward direction to throw
        Vector3 throwDirection = handTarget.forward;

        ball.useGravity = true;
        ball.AddForce(throwDirection * throwPower, ForceMode.VelocityChange);
    }
}
