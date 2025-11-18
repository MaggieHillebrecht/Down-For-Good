using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BallThrowController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private HandPickup handPickup;
    [SerializeField] private Transform handTarget;
    [SerializeField] private Camera cam;
    [SerializeField] private ChargedThrow chargeController;
    [SerializeField] private Animator anim;
    [SerializeField] private CrosshairController crosshair;
    [SerializeField] private HandTransparencyController handTransparency;

    [Header("Throw Settings")]
    [SerializeField] private float minThrowForce = 5f;
    [SerializeField] private float maxThrowForce = 25f;

    private Vector3 aimDirection;

    void Update()
    {
        if (!handPickup.IsHoldingBall())
            return;

        HandleInput();
    }

    private void HandleInput()
    {
        if (Input.GetMouseButtonDown(1))
        {
            chargeController.StartCharging();
            handTransparency?.SetVisible(false);
            crosshair?.SetVisible(true);
        }

        if (Input.GetMouseButton(1))
        {
            chargeController.UpdateCharge();

            // Update aiming
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            Vector3 worldAimPoint;

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                worldAimPoint = hit.point;
                aimDirection = (hit.point - handTarget.position).normalized; // for throw
            }
            else
            {
                worldAimPoint = cam.transform.position + cam.transform.forward * 100f;
                aimDirection = cam.transform.forward;
            }
            crosshair.UpdatePosition(worldAimPoint);
        }

        if (Input.GetMouseButtonUp(1))
        {
            chargeController.StopCharging();
            handTransparency?.SetVisible(true);
            crosshair?.SetVisible(false);

            StartCoroutine(HandleThrowAnimation());
            ThrowBall(chargeController.CurrentCharge);
            chargeController.ResetCharge();
        }
    }

    private void ThrowBall(float charge)
    {
        Rigidbody ball = handPickup.ReleaseBall();
        if (ball == null) return;

        float throwPower = Mathf.Lerp(minThrowForce, maxThrowForce, charge);
        Vector3 throwVelocity = aimDirection * throwPower;

        ball.useGravity = true;
        ball.linearVelocity = throwVelocity;
    }

    private IEnumerator HandleThrowAnimation()
    {
        anim.SetBool("Charging", true);
        anim.Update(0f);
        yield return null;

        anim.ResetTrigger("Throw");
        anim.SetTrigger("Throw");

        yield return null;
        anim.SetBool("Charging", false);
    }
}