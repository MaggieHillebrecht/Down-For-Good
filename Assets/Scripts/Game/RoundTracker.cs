using UnityEngine;
using UnityEngine.UI;

public class RoundTracker : MonoBehaviour
{
    public static RoundTracker Instance;

    [Header("UI")]
    [SerializeField] private Text roundText;         
    [SerializeField] private GameObject bossRoundUI; 

    private int currentRound = 1;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        UpdateRoundUI();
    }

    public void StartNextRound()
    {
        currentRound++;

        UpdateRoundUI();
    }

    public int GetCurrentRound()
    {
        return currentRound;
    }

    private void UpdateRoundUI()
    {
        if (roundText != null)
        {
            roundText.text = $"Round: {currentRound}";
        }

        if (bossRoundUI != null)
        {
            bossRoundUI.SetActive(currentRound % 3 == 0 && currentRound != 0);
        }
    }
}
