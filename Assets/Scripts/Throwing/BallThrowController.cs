using System.Collections;
using UnityEngine;
using UnityEngine.UI;

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
    [SerializeField] private float chargeSpeed = .5f; 

    [Header("UI")]
    [SerializeField] private Slider chargeSlider; 

    private float currentCharge = 0f; 
    private bool isCharging = false;
    private Vector3 aimDirection;
    private int chargeDirection = 1; // 1 = increasing, -1 = decreasing

    void Update()
    {
        if (!handPickup.IsHoldingBall())
            return;

        HandleCharging();
        UpdateChargeUI();
    }

    private void HandleCharging()
    {
        // Start charging
        if (Input.GetMouseButtonDown(1))
        {
            isCharging = true;
            currentCharge = 0f;
            chargeDirection = 1;

            anim?.SetBool("Charging", true);
            handTransparency?.SetVisible(false);
            crosshair?.SetVisible(true);
        }

        // Charging
        if (isCharging && Input.GetMouseButton(1))
        {
            // Ping-pong logic
            currentCharge += chargeDirection * chargeSpeed * Time.deltaTime;

            if (currentCharge >= 1f)
            {
                currentCharge = 1f;
                chargeDirection = -1; // start decreasing
            }
            else if (currentCharge <= 0f)
            {
                currentCharge = 0f;
                chargeDirection = 1; // start increasing
            }

            // Aim raycast
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
                aimDirection = (hit.point - handTarget.position).normalized;
            else
                aimDirection = handTarget.forward;

            crosshair?.UpdatePosition(handTarget.position + aimDirection * 10f);
        }

        // Release throw
        if (isCharging && Input.GetMouseButtonUp(1))
        {
            isCharging = false;

            handTransparency?.SetVisible(true);
            crosshair?.SetVisible(false);

            anim?.SetBool("Charging", false);
            StartCoroutine(HandleThrowAnimation());
            ThrowBall();

            currentCharge = 0f; // reset
            chargeDirection = 1;
        }
    }

    private void UpdateChargeUI()
    {
        if (chargeSlider != null)
        {
            chargeSlider.value = currentCharge; // Slider value 0 → 1
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

    private IEnumerator HandleThrowAnimation()
    {
        anim.SetBool("Charging", true);
        anim.Update(0f); // forces Animator to evaluate immediately

        yield return null; // let one frame pass

        anim.ResetTrigger("Throw");
        anim.SetTrigger("Throw");

        const int maxFramesToWait = 10;
        int frames = 0;
        int throwHash = Animator.StringToHash("ThrowingAnimation");

        while (frames < maxFramesToWait)
        {
            if (anim.IsInTransition(0))
            {
                var next = anim.GetNextAnimatorStateInfo(0);
                if (next.shortNameHash == throwHash || next.IsName("ThrowingAnimation"))
                    break;
            }
            frames++;
            yield return null;
        }

        anim.SetBool("Charging", false);
    }
}