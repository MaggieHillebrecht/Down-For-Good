using UnityEngine;

public class Pins : MonoBehaviour
{
    [SerializeField] private int scoreValue = 1;
    [SerializeField] private Color hitColor = Color.black;

    private bool hasScored = false;
    private Color originalColor;
    private Renderer rend;

    private void Awake()
    {
        rend = GetComponent<Renderer>();
        if (rend != null)
        {
            rend.material = new Material(rend.material);
            originalColor = rend.material.color;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ball") && !hasScored)
        {
            hasScored = true;

            if (ScoreManager.Instance != null)
                ScoreManager.Instance.AddScore(scoreValue);

            if (rend != null)
                rend.material.color = hitColor;
        }
    }

    public void ResetPin()
    {
        hasScored = false;
        if (rend != null)
            rend.material.color = originalColor;
    }
}