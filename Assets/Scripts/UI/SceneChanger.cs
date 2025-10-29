using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneChanger : MonoBehaviour
{
    [SerializeField] private Animation anim; 
    [SerializeField] private string nextScene = "Game";

    public void OnMainGameButtonClick()
    {
        if (anim != null && anim.clip != null)
        {
            anim.Play();
            StartCoroutine(WaitAndLoadScene(anim.clip.length));
        }
        else
        {
            SceneManager.LoadScene(nextScene);
        }
    }

    public void OnMainMenuClick()
    {
        SceneManager.LoadScene("FrontEndMenu");
    }

    public void OnQuitClick()
    {
        Application.Quit();
    }

    private IEnumerator WaitAndLoadScene(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(nextScene);
    }
}