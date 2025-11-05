using UnityEngine;

public interface IState
{
    void OnEnter();
    void UpdateState();
    void OnExit();
}

public class StateController : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private GameObject pauseMenuUI;

    private IState currentState;

    void Start()
    {
        // Start in Play mode
        ChangeState(new PlayState(this, pauseMenuUI));
    }

    void Update()
    {
        currentState?.UpdateState();
    }

    public void ChangeState(IState newState)
    {
        currentState?.OnExit();
        currentState = newState;
        currentState?.OnEnter();
    }

    // Called by buttons
    public void PauseGame()
    {
        ChangeState(new PauseState(this, pauseMenuUI));
    }

    public void ResumeGame()
    {
        ChangeState(new PlayState(this, pauseMenuUI));
    }
}