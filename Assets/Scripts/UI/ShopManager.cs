using UnityEngine;
using System.Collections;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private UIFader shopFader;
    [SerializeField] private GameObject hudUI;
    [SerializeField] private GameObject loserScreen;
    [SerializeField] private Animator cameraAnimator;
    [SerializeField] private Timer timerInstance;
    [SerializeField] private string lookToShopAnim = "LookToShop";
    [SerializeField] private string lookToGameAnim = "LookToGame";

    private void OnEnable()
    {
        StartCoroutine( SubscribeWhenReady() );
    }

    private IEnumerator SubscribeWhenReady()
    {
        while ( BasicArcadeGameLogic.Instance == null )
        {
            yield return null;
        }

        BasicArcadeGameLogic.Instance.OnRoundSuccess += HandleRoundSuccess;
        BasicArcadeGameLogic.Instance.OnRoundFailed += HandleRoundFailed;
    }

    private void OnDestroy()
    {
        // unsubscribes when destroyed
        if ( BasicArcadeGameLogic.Instance != null )
        {
            BasicArcadeGameLogic.Instance.OnRoundSuccess -= HandleRoundSuccess;
            BasicArcadeGameLogic.Instance.OnRoundFailed -= HandleRoundFailed;
        }
    }

    private void HandleRoundSuccess()
    {
        StartCoroutine( PlayCameraAnimAndOpenShop() );
    }

    private void HandleRoundFailed()
    {
        if ( hudUI != null ) hudUI.SetActive( false );
        if ( loserScreen != null ) loserScreen.SetActive( true );
    }

    private IEnumerator PlayCameraAnimAndOpenShop()
    {
        // Hide HUD
        if ( hudUI != null )
            hudUI.SetActive( false );

        // Activate the shop before fading
        if ( shopFader != null )
            shopFader.gameObject.SetActive( true );

        // Play camera animation
        if ( cameraAnimator != null && !string.IsNullOrEmpty( lookToShopAnim ) )
        {
            cameraAnimator.Play( lookToShopAnim );
            float animLength = GetAnimationClipLength( lookToShopAnim );
            yield return new WaitForSeconds( animLength );
        }

        // Fade in the shop UI
        if ( shopFader != null )
            shopFader.FadeIn();
    }

    public IEnumerator ReturnFromShop()
    {
        // Fade out the shop UI
        if ( shopFader != null )
            shopFader.FadeOut();

        if ( shopFader != null )
        {
            yield return new WaitForSeconds( shopFader.fadeDuration );
        }

        // Play camera animation back to the game
        if ( cameraAnimator != null && !string.IsNullOrEmpty( lookToGameAnim ) )
        {
            cameraAnimator.Play( lookToGameAnim );
            float animLength = GetAnimationClipLength( lookToGameAnim );
            yield return new WaitForSeconds( animLength );
        }

        if ( timerInstance != null )
            timerInstance.StartTimer();

        if ( shopFader != null )
            shopFader.gameObject.SetActive( false );

        if ( hudUI != null )
            hudUI.SetActive( true );

        BasicArcadeGameLogic.Instance.StartGame();
    }

    public void ExitShopAndReturnToGame()
    {
        StartCoroutine( ReturnFromShop() );
    }

    private float GetAnimationClipLength( string clipName )
    {
        if ( cameraAnimator == null )
        {
            return 0f;    
        } 

        foreach ( var clip in cameraAnimator.runtimeAnimatorController.animationClips )
        {
            if ( clip.name == clipName )
                return clip.length;
        }

        return 0f;
    }
    
}