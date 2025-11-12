using UnityEngine;

public class Tools : MonoBehaviour
{
    [SerializeField] private BasicArcadeGameLogic gameLogic;
    [SerializeField] private ShopManager shopManager;
    [SerializeField] private HandPickup playerHand;
    [SerializeField] private bool destroyHeldBall = true;

    public void GoToShopDev()
    {
        if (gameLogic.IsGameActive())
        {
            gameLogic.EndGameByTimer();
        }

        if (destroyHeldBall && playerHand != null)
        {
            playerHand.DestroyHeldBall();
        }

        gameLogic.ForceRoundSuccess();
    }
}
