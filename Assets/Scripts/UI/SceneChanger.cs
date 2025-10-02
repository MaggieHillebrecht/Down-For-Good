using UnityEngine;

public class SceneChanger : MonoBehaviour
{
    public void OnTestOneButtonClick()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
    }

    public void OnBackSpaceClick()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("FrontEndMenu");

    }

    public void OnQuitClick()
    {
        Application.Quit();
    }
}
