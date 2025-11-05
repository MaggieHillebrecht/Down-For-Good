using UnityEngine;

public class PlayState : IState
{
    private readonly StateController controller;
    private readonly GameObject pauseMenuUI;

    public PlayState(StateController controller, GameObject pauseMenuUI)
    {
        this.controller = controller;
        this.pauseMenuUI = pauseMenuUI;
    }

    public void OnEnter()
    {
        Time.timeScale = 1f;
        if (pauseMenuUI != null)
            pauseMenuUI.SetActive(false);
    }

    public void UpdateState() { }

    public void OnExit() { }
}