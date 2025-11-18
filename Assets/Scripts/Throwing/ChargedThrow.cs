using System.Collections;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class ChargedThrow : MonoBehaviour
{
    [SerializeField] private float chargeSpeed = 0.5f;
    [SerializeField] private Animator anim;
    [SerializeField] private TwoBoneIKConstraint rightArmIK;

    private float currentCharge = 0f;
    private int chargeDirection = 1;
    private bool isCharging = false;

    public float CurrentCharge => currentCharge;
    public bool IsCharging => isCharging;

    void Update()
    {
        if (isCharging)
            UpdateCharge();
    }

    public void StartCharging()
    {
        isCharging = true;

        currentCharge = 0f;
        chargeDirection = 1;

        // Disable IK so animation fully controls the arm
        if (rightArmIK != null)
            rightArmIK.weight = 0f;

        anim.SetBool("Charging", true);
        anim.speed = 1f;
    }

    public void UpdateCharge()
    {
        currentCharge += chargeDirection * chargeSpeed * Time.deltaTime;

        if (currentCharge >= 1f)
        {
            currentCharge = 1f;
            chargeDirection = -1;
        }
        else if (currentCharge <= 0f)
        {
            currentCharge = 0f;
            chargeDirection = 1;
        }
    }

    public void StopCharging()
    {
        isCharging = false;

        anim.SetBool("Charging", false);
        anim.speed = 1f;
    }

    public void ResetCharge()
    {
        currentCharge = 0f;
        chargeDirection = 1;
    }
    
    public void EnableIKAfterThrowDelay()
    {
        StartCoroutine(EnableIKDelayed());
    }

    private IEnumerator EnableIKDelayed()
    {
        // Small delay prevents IK from overriding first frames of throw animation
        yield return new WaitForSeconds(0.15f);
        rightArmIK.weight = 1f;
    }
}