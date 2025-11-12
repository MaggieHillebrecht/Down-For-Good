using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [Header("Timer Settings")]
    [SerializeField] private float gameDuration = 30f;

    [Header("UI References")]
    [SerializeField] private Text timerText;

    private float timeRemaining;
    private bool timerRunning = false;

    void Start()
    {
        StartTimer();
    }

    void Update()
    {
        if (!timerRunning)
            return;

        timeRemaining -= Time.deltaTime;
        if (timeRemaining <= 0f)
        {
            timeRemaining = 0f;
            timerRunning = false;
            BasicArcadeGameLogic.Instance.EndGameByTimer();
        }

        UpdateTimerUI();
    }

    public void StartTimer()
    {
        timeRemaining = gameDuration;
        timerRunning = true;
        BasicArcadeGameLogic.Instance.StartGame();
    }

    private void UpdateTimerUI()
    {
        if (timerText != null)
            timerText.text = $"Time: {Mathf.CeilToInt(timeRemaining)}";
    }
}