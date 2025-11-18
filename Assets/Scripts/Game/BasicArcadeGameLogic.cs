using UnityEngine;
using System;

public class BasicArcadeGameLogic : MonoBehaviour
{
    public static BasicArcadeGameLogic Instance;

    private bool isGameActive = false;

    public event Action OnRoundSuccess;
    public event Action OnRoundFailed;
    [SerializeField] private HandPickup playerHand;

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
        
        foreach (var pin in FindObjectsOfType<Pins>())
        {
            pin.ResetPin();
        }
    }

    public void EndGameByTimer()
    {
        if (!isGameActive)
            return;

        isGameActive = false;

        if (playerHand != null)
        {
            playerHand.DestroyHeldBall();
        }

        if (ScoreManager.Instance.CurrentScore >= ScoreManager.Instance.ScoreGoal)
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
        ScoreManager.Instance.StartRound(true);
        StartGame();
    }

    public void ForceRoundSuccess()
    {
        isGameActive = false;
        OnRoundSuccess?.Invoke();
    }

    public void ForceRoundFail()
    {
        isGameActive = false;
        OnRoundFailed?.Invoke();
    }

}