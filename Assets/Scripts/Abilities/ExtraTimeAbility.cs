using UnityEngine;

[CreateAssetMenu(fileName = "ExtraTimeAbility", menuName = "Scriptable Objects/ExtraTimeAbility")]
public class ExtraTimeAbility : Ability
{
    public float extraTime = 15f;

    private Timer timer;

    public void SetTimer(Timer sceneTimer)
    {
        timer = sceneTimer;
    }

    public override void ApplyAbility()
    {
        if (timer != null)
        {
            timer.AddTime(extraTime);
        }
    }
}