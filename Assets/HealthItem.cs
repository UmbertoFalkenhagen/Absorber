using UnityEngine;

public class HealthItem : MonoBehaviour
{
    public int healthAmount = 2; // Amount of health to restore

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.GainHealth(healthAmount);
                Destroy(gameObject); // Destroy the health item after use
            }
        }
    }
}