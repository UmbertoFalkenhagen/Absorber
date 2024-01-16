using System.Collections;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxhp = 5;
    public int currenthp;

    public float shakeDuration = 0.1f;
    public float shakeMagnitude = 0.05f;

    public Material defaultMaterial;
    public Material lowHPMaterial; // Set this in the Inspector to the material for HP <= 5
    public Material criticalHPMaterial; // Set this in the Inspector to the material for HP <= 2

    public Renderer objectRenderer;

    private void Start()
    {
        currenthp = maxhp;
        objectRenderer.material = defaultMaterial;
    }

    public void Update()
    {
        // Check for material change conditions
        if (currenthp <= maxhp / 4)
        {
            objectRenderer.material = criticalHPMaterial;
        }
        else if (currenthp <= maxhp / 2)
        {
            objectRenderer.material = lowHPMaterial;
        } 
        
        if (currenthp <= 0)
        {
            HighscoreManager.Instance.SetHighScore(maxhp);
            DestroyEnemy();
        }
    }

    public void TakeDamage(int damage)
    {
        currenthp -= damage;
        Debug.Log("Taking damage, new hp is " + currenthp);
        // Trigger shake effect
        Shake();

        
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