using UnityEngine;

public enum AbilityType
{
    DoubleScore,
    ExtraTime
}

[CreateAssetMenu(fileName = "New Ability", menuName = "Shop/Ability")]
public class Ability : ScriptableObject
{
    public string abilityName;
    public AbilityType abilityType;
    public string description;

    public virtual void ApplyAbility()
    {
        switch (abilityType)
        {
            case AbilityType.DoubleScore:
                ScoreManager.Instance.scoreMultiplier = 2f;
                break;
            case AbilityType.ExtraTime:
                if (BasicArcadeGameLogic.Instance != null && BasicArcadeGameLogic.Instance.GetComponent<Timer>() != null)
                {
                    BasicArcadeGameLogic.Instance.GetComponent<Timer>().AddTime(15f);
                }
                break;
        }
    }
}