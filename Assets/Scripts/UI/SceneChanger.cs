using UnityEngine;

public class SceneChanger : MonoBehaviour
{
    public void OnTestOneButtonClick()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
    }

    public void OnTestTwoButtonClick()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("TestTwo");
    }

    public void OnTestThreeButtonClick()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("TestThree");
    }

    public void OnBackSpaceClick()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("FrontEndMenu");

    }
}
