using System.Collections;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    public float shakeDuration = 0.1f;
    public float shakeMagnitude = 0.05f;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        // Trigger shake effect
        Shake();

        if (currentHealth <= 0)
        {
            DestroyEnemy();
        }
    }

    private void Shake()
    {
        // Start shaking coroutine
        StartCoroutine(ShakeCoroutine());
    }

    private IEnumerator ShakeCoroutine()
    {
        float elapsed = 0.0f;
        Vector3 originalPosition = transform.position;

        while (elapsed < shakeDuration)
        {
            float x = Random.Range(-1f, 1f) * shakeMagnitude;
            float y = Random.Range(-1f, 1f) * shakeMagnitude;

            transform.position = new Vector3(originalPosition.x + x, originalPosition.y + y, originalPosition.z);

            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = originalPosition;
    }

    private void DestroyEnemy()
    {
        // Implement any logic you need when the enemy is destroyed
        // For example, play an explosion animation, spawn particles, etc.

        // Then, destroy the GameObject
        Destroy(gameObject);
    }
}