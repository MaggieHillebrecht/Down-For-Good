using UnityEngine;

[CreateAssetMenu(fileName = "DoubleScoreAbility", menuName = "Scriptable Objects/DoubleScoreAbility")]
public class DoubleScoreAbility : Ability
{
    public float multiplier = 2f;

    public override void ApplyAbility()
    {
        ScoreManager.Instance.scoreMultiplier *= multiplier;
    }
}
