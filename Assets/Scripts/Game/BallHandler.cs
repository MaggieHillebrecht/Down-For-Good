using UnityEngine;
using System.Collections;

public class BallHandler : MonoBehaviour
{
    [Header("Spawn Settings")]
    [SerializeField] private GameObject ballPrefab; // Assign your ball prefab
    [SerializeField] private Vector3 spawnPosition = new Vector3(0.74f, -1.02f, -0.8f);

    [Header("Fail Settings")]
    [SerializeField] private float destroyYThreshold = -20f; // if ball falls below this
    [SerializeField] private float respawnDelay = 5f; // small delay to prevent collisions immediately

    private bool isRespawning = false; // prevent multiple respawns at the same time

    void Update()
    {
        // Destroy and respawn if the ball falls below threshold
        if (!isRespawning && transform.position.y < destroyYThreshold)
        {
            StartCoroutine(RespawnBall());
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // Destroy and respawn if it hits anything that is NOT the ground
        if (!isRespawning && !collision.collider.CompareTag("Ground"))
        {
            StartCoroutine(RespawnBall());
        }
    }

    private IEnumerator RespawnBall()
    {
        isRespawning = true;

        // Optionally disable collider and renderer to prevent further collisions/visual glitches
        Collider col = GetComponent<Collider>();
        if (col != null) col.enabled = false;

        Renderer rend = GetComponent<Renderer>();
        if (rend != null) rend.enabled = false;

        // Wait a short delay to prevent collision loops
        yield return new WaitForSeconds(respawnDelay);

        // Spawn new ball
        if (ballPrefab != null)
        {
            Instantiate(ballPrefab, spawnPosition, Quaternion.identity);
        }

        // Destroy this ball
        Destroy(gameObject);
    }
}