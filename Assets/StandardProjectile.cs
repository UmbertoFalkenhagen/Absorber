using UnityEngine;

public class StandardProjectile : Projectile
{
    public LayerMask wallLayer; // Set this in the Inspector to the "wall" layer.

    protected override void OnTriggerEnter(Collider other)
    {
        // Check for InteractiveObject component
        InteractiveObject interactiveObject = other.gameObject.GetComponent<InteractiveObject>();
        if (interactiveObject != null)
        {
            interactiveObject.TakeDamage(1);
            Destroy(gameObject);  // Optionally, destroy the bullet after hitting.
            return; // Exit the method to avoid further checks.
        }

        // Check for EnemyHealth component
        EnemyHealth enemyHealth = other.gameObject.GetComponent<EnemyHealth>();
        if (enemyHealth != null)
        {
            enemyHealth.TakeDamage(1);
            Destroy(gameObject);  // Optionally, destroy the bullet after hitting.
            return; // Exit the method to avoid further checks.
        }

        // If the collided object is on the "wall" layer
        if (wallLayer == (wallLayer | (1 << other.gameObject.layer)))
        {
            Destroy(gameObject);
        }
    }
}