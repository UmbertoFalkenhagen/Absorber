using UnityEngine;

public class StandardProjectile : Projectile
{
    public LayerMask wallLayer; // Set this in the Inspector to the "wall" layer.


    protected override void OnTriggerEnter(Collider other)
    {
        if (parent == 0)
        {
            InteractiveObject interactiveObject = other.gameObject.GetComponent<InteractiveObject>();
            if (interactiveObject != null)
            {
                interactiveObject.TakeDamage(damage);
                Destroy(gameObject);  // Optionally, destroy the bullet after hitting.
                Debug.Log("Object collision");
            }

            // Check for EnemyHealth component
            EnemyHealth enemyHealth = other.gameObject.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
                Destroy(gameObject);  // Optionally, destroy the bullet after hitting.
                Debug.Log("Enemy collision");
                return; // Exit the method to avoid further checks.
            }
        } else if ( parent == 1)
        {
            if (playerHealth == null)
            {
                Debug.Log("No player health assigned");
                return;
            }
            playerHealth.TakeDamage(damage);
            Destroy(gameObject);  // Optionally, destroy the bullet after hitting.
            Debug.Log("Player collision");
            return; // Exit the method to avoid further checks.
        } else
        {
            Debug.Log("Wrong parent setting");
        }
        

        // If the collided object is on the "wall" layer
        if (wallLayer == (wallLayer | (1 << other.gameObject.layer)))
        {
            Destroy(gameObject);
        }
    }
}