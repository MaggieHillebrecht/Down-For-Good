using UnityEngine;

public class PauseState : IState
{
    private readonly StateController controller;
    private readonly GameObject pauseMenuUI;

    public PauseState(StateController controller, GameObject pauseMenuUI)
    {
        this.controller = controller;
        this.pauseMenuUI = pauseMenuUI;
    }

    public void OnEnter()
    {
        Time.timeScale = 0f;
        if (pauseMenuUI != null)
            pauseMenuUI.SetActive(true);
    }

    public void UpdateState() { }

    public void OnExit()
    {
        Time.timeScale = 1f;
        if (pauseMenuUI != null)
            pauseMenuUI.SetActive(false);
    }
}