using UnityEngine;

public class Pins : MonoBehaviour
{
    [SerializeField] private int scoreValue = 1;

    private bool hasScored = false;

    private void OnCollisionEnter(Collision collision)
    {
        // Only trigger if hit by something relevant (e.g. a ball)
        if (collision.gameObject.CompareTag("Ball") && !hasScored)
        {
            hasScored = true;

            // Add score through your ScoreManager
            if (ScoreManager.Instance != null)
            {
                ScoreManager.Instance.AddScore(scoreValue);
            }
        }
    }

    // Optional: reset scoring when pin is reset/upright again
    public void ResetPin()
    {
        hasScored = false;
    }
}
