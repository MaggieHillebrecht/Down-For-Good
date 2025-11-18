using UnityEngine;

public class ExtraTimeAbility : Ability
{
    public float extraTime = 15f;

    public override void ApplyAbility()
    {
        Timer timer = Object.FindObjectOfType<Timer>();
        if (timer != null)
        {
            timer.AddTime(extraTime);
        }
    }
}
