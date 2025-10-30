using UnityEngine;
using UnityEngine.UI;
using System;

public class BasicArcadeGameLogic : MonoBehaviour
{
    public static BasicArcadeGameLogic Instance;

    [Header("Score Settings")]
    [SerializeField] private int scoreGoal = 20;

    [Header("Timer Settings")]
    [SerializeField] private float roundDuration = 15f; 
    [SerializeField] private Text timerText;

    [Header("UI References")]
    [SerializeField] private Text scoreText;

    private int currentScore = 0;
    private float timer = 0f;
    private bool isGameActive = false;

    public event Action OnRoundSuccess;
    public event Action OnRoundFailed;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy( gameObject );
        }
    }

    void Start()
    {
        UpdateScoreUI();
        UpdateTimerUI();
    }

    public void StartGame()
    {
        currentScore = 0;
        timer = roundDuration;
        isGameActive = true;

        UpdateScoreUI();
        UpdateTimerUI();
    }

    public void EndGameByTimer()
    {
        if ( !isGameActive )
        {
            return;
        }
        else
        {
            isGameActive = false;
        }

        if ( currentScore >= scoreGoal )
        {
            OnRoundSuccess?.Invoke();
        }
        else
        {
            OnRoundFailed?.Invoke();
        }
    }

    public void AddScore( int amount )
    {
        if ( !isGameActive )
        {
            return;
        }

        currentScore += amount;
        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        if ( scoreText != null )
            scoreText.text = $"Score: {currentScore}";
    }

    private void UpdateTimerUI()
    {
        if ( timerText != null )
            timerText.text = $"Time: {Mathf.CeilToInt( timer )}";
    }

    public void ExitShopAndStartNextRound()
    {
        StartGame();
    }
}