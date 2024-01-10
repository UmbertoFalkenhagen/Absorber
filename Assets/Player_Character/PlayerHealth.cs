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
        SoundManager.Instance.PlaySoundOnce("TakeDamage");


        if (currentHealth <= 0)
        {
            KillPlayer();
        }

        healthbar.SetHealth(currentHealth);
    }

    private void KillPlayer()
    {
        GameManager.Instance.GameOver();
    }

    public void GainHealth(int amount)
    {
        currentHealth += amount;
        healthbar.SetHealth(currentHealth);
        SoundManager.Instance.PlaySoundOnce("GainHealth");
    }
}
