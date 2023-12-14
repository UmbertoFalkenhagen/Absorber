using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public HealthBar healthbar;

    public int maxHealth = 10;
    private int currentHealth;

    private void Awake()
    {
        
    }

    private void Start()
    {
        healthbar.SetMaxHealth(maxHealth);
        currentHealth = maxHealth;
        healthbar.SetHealth(currentHealth);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;


        if (currentHealth <= 0)
        {
            KillPlayer();
        }

        healthbar.SetHealth(currentHealth);
    }

    private void KillPlayer()
    {
        // Implement any logic you need when the enemy is destroyed
        // For example, play an explosion animation, spawn particles, etc.

        // Then, destroy the GameObject
        Destroy(gameObject);
    }

    public void GainHealth(int amount)
    {
        currentHealth += amount;
        healthbar.SetHealth(currentHealth);
    }
}
