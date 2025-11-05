using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    [Header("UI References")]
    [SerializeField] private Text scoreText;

    public int CurrentScore { get; private set; } = 0;

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
    }

    public void AddScore(int amount)
    {
        if (!BasicArcadeGameLogic.Instance.IsGameActive())
            return;

        CurrentScore += amount;
        UpdateScoreUI();
    }

    public void ResetScore()
    {
        CurrentScore = 0;
        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
            scoreText.text = $"Score: {CurrentScore}";
    }
}