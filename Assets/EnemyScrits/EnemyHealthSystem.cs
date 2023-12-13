using System.Collections;
using UnityEngine;

public class EnemyHealthSystem : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    public float shakeDuration = 0.1f;
    public float shakeMagnitude = 0.05f;

    public GameObject destructionEffectPrefab;

    private Rigidbody enemyRigidbody;
    private Renderer enemyRenderer;

    private void Start()
    {
        currentHealth = maxHealth;

        enemyRigidbody = GetComponent<Rigidbody>();
        enemyRenderer = GetComponent<Renderer>();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        // Trigger shake effect
        Shake();

        // Check if the enemy is destroyed
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

            enemyRigidbody.MovePosition(new Vector3(originalPosition.x + x, originalPosition.y + y, originalPosition.z));

            elapsed += Time.deltaTime;
            yield return null;
        }

        enemyRigidbody.MovePosition(originalPosition);
    }

    private void DestroyEnemy()
    {
        // Instantiate destruction effect
        Instantiate(destructionEffectPrefab, transform.position, Quaternion.identity);

        // Destroy the enemy GameObject
        Destroy(gameObject);
    }
}