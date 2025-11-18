using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    [Header("UI References")]
    [SerializeField] private Text scoreText;

    [Header("Score Goal Settings")]
    [SerializeField] private int initialScoreGoal = 5; // starting goal
    [SerializeField] private int scoreGoalIncrement = 2; // how much goal increases each round

    public int CurrentScore { get; private set; } = 0;
    private int scoreGoal;

    public int ScoreGoal => scoreGoal; 
    public float scoreMultiplier { get; set; } = 1f;


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

        scoreGoal = initialScoreGoal; // initialize first round
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

    public void StartRound(bool increaseGoal = true)
    {
        if (increaseGoal)
            scoreGoal += scoreGoalIncrement;

        ResetScore();
        UpdateScoreUI();
    }

    public void UpdateScoreUI()
    {
        if (scoreText != null)
            scoreText.text = $"Score: {CurrentScore} / {scoreGoal}";
    }

}