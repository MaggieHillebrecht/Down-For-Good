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
    [SerializeField] private HandPickup playerHand;
    [SerializeField] private Ability[] abilitiesForSale;
    private Ability lastPurchasedAbility;



    private void OnEnable()
    {
        StartCoroutine(SubscribeWhenReady());
    }

    private IEnumerator SubscribeWhenReady()
    {
        while (BasicArcadeGameLogic.Instance == null)
        {
            yield return null;
        }

        BasicArcadeGameLogic.Instance.OnRoundSuccess += HandleRoundSuccess;
        BasicArcadeGameLogic.Instance.OnRoundFailed += HandleRoundFailed;
    }

    private void OnDestroy()
    {
        // unsubscribes when destroyed
        if (BasicArcadeGameLogic.Instance != null)
        {
            BasicArcadeGameLogic.Instance.OnRoundSuccess -= HandleRoundSuccess;
            BasicArcadeGameLogic.Instance.OnRoundFailed -= HandleRoundFailed;
        }
    }

    private void HandleRoundSuccess()
    {
        StartCoroutine(PlayCameraAnimAndOpenShop());
    }

    private void HandleRoundFailed()
    {
        if (hudUI != null) hudUI.SetActive(false);
        if (loserScreen != null) loserScreen.SetActive(true);
    }

    private IEnumerator PlayCameraAnimAndOpenShop()
    {
        // Destroy ball in hand if any
        if (playerHand != null && playerHand.IsHoldingBall())
        {
            playerHand.DestroyHeldBall();
        }

        // Hide HUD
        if (hudUI != null)
            hudUI.SetActive(false);

        // Activate the shop before fading
        if (shopFader != null)
            shopFader.gameObject.SetActive(true);

        // Play camera animation
        if (cameraAnimator != null && !string.IsNullOrEmpty(lookToShopAnim))
        {
            cameraAnimator.Play(lookToShopAnim);
            float animLength = GetAnimationClipLength(lookToShopAnim);
            yield return new WaitForSeconds(animLength);
        }

        // Fade in the shop UI
        if (shopFader != null)
            shopFader.FadeIn();
    }

    public IEnumerator ReturnFromShop()
    {
        if (shopFader != null)
            shopFader.FadeOut();
        yield return new WaitForSeconds(shopFader.fadeDuration);

        if (cameraAnimator != null && !string.IsNullOrEmpty(lookToGameAnim))
        {
            cameraAnimator.Play(lookToGameAnim);
            yield return new WaitForSeconds(GetAnimationClipLength(lookToGameAnim));
        }

        timerInstance.StartTimer();
        lastPurchasedAbility?.ApplyAbility();

        shopFader.gameObject.SetActive(false);
        hudUI.SetActive(true);

        BasicArcadeGameLogic.Instance.ExitShopAndStartNextRound();
    }

    public void ExitShopAndReturnToGame()
    {
        StartCoroutine(ReturnFromShop());
    }

    private float GetAnimationClipLength(string clipName)
    {
        if (cameraAnimator == null)
        {
            return 0f;
        }

        foreach (var clip in cameraAnimator.runtimeAnimatorController.animationClips)
        {
            if (clip.name == clipName)
                return clip.length;
        }

        return 0f;
    }
    public void BuyAbility(int index)
    {
        if (index < 0 || index >= abilitiesForSale.Length)
            return;

        lastPurchasedAbility = abilitiesForSale[index];

        if (lastPurchasedAbility is ExtraTimeAbility extraTimeAbility)
        {
            extraTimeAbility.SetTimer(timerInstance);
        }
    }
}