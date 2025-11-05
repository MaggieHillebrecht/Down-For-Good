using UnityEngine;
using System;

public class BasicArcadeGameLogic : MonoBehaviour
{
    public static BasicArcadeGameLogic Instance;

    [Header("Score Settings")]
    [SerializeField] private int scoreGoal = 20;

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
            Destroy(gameObject);
        }
    }

    public void StartGame()
    {
        isGameActive = true;
        ScoreManager.Instance.ResetScore();
    }

    public void EndGameByTimer()
    {
        if (!isGameActive)
            return;

        isGameActive = false;

        if (ScoreManager.Instance.CurrentScore >= scoreGoal)
        {
            OnRoundSuccess?.Invoke();
        }
        else
        {
            OnRoundFailed?.Invoke();
        }
    }

    public bool IsGameActive()
    {
        return isGameActive;
    }

    public void ExitShopAndStartNextRound()
    {
        StartGame();
    }
}