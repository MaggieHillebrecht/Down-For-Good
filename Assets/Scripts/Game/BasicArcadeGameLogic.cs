using UnityEngine;
using UnityEngine.UI;

public class BasicArcadeGameLogic : MonoBehaviour
{
    public static BasicArcadeGameLogic Instance; // Created a Singleton for easy access

    [Header("Score Settings")]
    [SerializeField] private int scorePerPin = 1;

    [Header("UI References")]
    [SerializeField] private Text scoreText; 

    private int currentScore = 0;
    private bool isGameActive = false;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        UpdateScoreUI();
    }

    public void StartGame()
    {
        currentScore = 0;
        isGameActive = true;
        UpdateScoreUI();
    }

    public void EndGame()
    {
        isGameActive = false;
    }

    public void AddScore(int amount)
    {
        if (!isGameActive) return;

        currentScore += amount;
        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
            scoreText.text = $"Score: {currentScore}";
    }

    // Optional for colored pin multipliers later
    public void AddColoredPinScore(string colorTag)
    {
        if (!isGameActive) return;

        int points = colorTag switch
        {
            "Pin" => scorePerPin * 3,
            "BluePin" => scorePerPin * 2,
            _ => scorePerPin
        };

        AddScore(points);
    }
}
