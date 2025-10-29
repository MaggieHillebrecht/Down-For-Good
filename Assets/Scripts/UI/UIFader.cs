using UnityEngine;
using System.Collections;

public class UIFader : MonoBehaviour
{
    [SerializeField] public float fadeDuration = 0.5f;
    [SerializeField] private CanvasGroup canvasGroup;
    private Coroutine fadeRoutine;

    private void Awake()
    {
        if ( canvasGroup == null )
            canvasGroup = GetComponent<CanvasGroup>();
    }

    public void FadeIn()
    {
        if ( fadeRoutine != null ) StopCoroutine( fadeRoutine );
        fadeRoutine = StartCoroutine( FadeCanvasGroup( 0f, 1f, true ) );
    }

    public void FadeOut()
    {
        if ( fadeRoutine != null ) StopCoroutine( fadeRoutine );
        fadeRoutine = StartCoroutine( FadeCanvasGroup( 1f, 0f, false ) );
    }

    private IEnumerator FadeCanvasGroup( float from, float to, bool enableInputAfter )
    {
        float elapsed = 0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;

        while ( elapsed < fadeDuration )
        {
            elapsed += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp( from, to, elapsed / fadeDuration );
            yield return null;
        }

        canvasGroup.alpha = to;
        canvasGroup.interactable = enableInputAfter;
        canvasGroup.blocksRaycasts = enableInputAfter;
    }
}
