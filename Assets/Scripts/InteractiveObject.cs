using System.Collections;
using UnityEngine;

public class InteractiveObject : MonoBehaviour
{
    public int maxhp = 10;
    public int currenthp;
    public GameObject destructionEffectPrefab;
    public GameObject healthEffectsPrefab;
    public GameObject healthItemPrefab;

    public Material defaultMaterial;
    public Material lowHPMaterial; // Set this in the Inspector to the material for HP <= 5
    public Material criticalHPMaterial; // Set this in the Inspector to the material for HP <= 2

    private Renderer objectRenderer;

    private Vector3 originalPosition;
    private bool isShaking = false;

    void Start()
    {
        currenthp = maxhp;
        objectRenderer = GetComponent<Renderer>();
        originalPosition = transform.position;
    }
    private void Update()
    {
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
            // 10% chance to spawn a health item
            if (Random.Range(1, 11) <= 2) // Generates a number between 1 and 10
            {
                SpawnHealthItem(SoundManager.Instance.GetSoundLength("HealthDrop"));
                Instantiate(healthItemPrefab, transform.position, Quaternion.identity);
            }
            Instantiate(destructionEffectPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject); // or any other effect like play animation, sound, etc.
        }
    }

    public void TakeDamage(int damage)
    {
        
        currenthp -= damage;
        Debug.Log("Taking damage, new hp is " + currenthp);
        // Trigger shake effect
        if (!isShaking)
            StartCoroutine(Shake());

        // Check for material change conditions
        
    }

    private IEnumerator Shake()
    {
        isShaking = true;
        float elapsed = 0.0f;
        float shakeDuration = 0.5f; // Duration of the shake effect
        float shakeMagnitude = 0.1f; // Magnitude of the shake effect

        while (elapsed < shakeDuration)
        {
            float x = Random.Range(-1f, 1f) * shakeMagnitude;
            float y = Random.Range(-1f, 1f) * shakeMagnitude;
            transform.position = new Vector3(originalPosition.x + x, originalPosition.y + y, originalPosition.z);

            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = originalPosition;
        isShaking = false;
    }

    private IEnumerator SpawnHealthItem(float delay)
    {
        SoundManager.Instance.PlaySoundOnce("HealthDrop");
        yield return new WaitForSeconds(delay);
        Instantiate(healthItemPrefab, transform.position, Quaternion.identity);
    }
}