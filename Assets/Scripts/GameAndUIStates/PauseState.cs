using UnityEngine;

public class PauseState : IState
{
    public void OnEnter()
    {
        //pause the game
    }

    public void UpdateState()
    {
        //Stay paused
    }

    public void OnExit()
    {
        //Resume the game
    }
}
