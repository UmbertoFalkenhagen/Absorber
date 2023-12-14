using UnityEngine;

public class RicochetProjectile : Projectile
{
    public LayerMask wallLayer; // Set this in the Inspector to the "wall" layer.
    public int maxRicochets = 2; // Maximum number of ricochets before being destroyed.

    public int currentRicochetCount = 0;

    private void OnCollisionEnter(Collision collision)
    {
        if (parent == 0)
        {
            InteractiveObject interactiveObject = collision.gameObject.GetComponent<InteractiveObject>();
            if (interactiveObject != null)
            {
                interactiveObject.TakeDamage(damage);
                Destroy(gameObject);  // Optionally, destroy the bullet after hitting.
                Debug.Log("Object collision");
            }

            // Check for EnemyHealth component
            EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage);
                Destroy(gameObject);  // Optionally, destroy the bullet after hitting.
                Debug.Log("Enemy collision");
                return; // Exit the method to avoid further checks.
            }

            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                Destroy(gameObject);  // Optionally, destroy the bullet after hitting.

                return; // Exit the method to avoid further checks.
            }
        }
        else if (parent == 1)
        {
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
                Destroy(gameObject);  // Optionally, destroy the bullet after hitting.

                return; // Exit the method to avoid further checks.
            }
        }

        if (wallLayer == (wallLayer | (1 << collision.gameObject.layer)))
        {
            currentRicochetCount++;

            if (currentRicochetCount > maxRicochets)
            {
                Destroy(gameObject);
            }
            else
            {
                // Adjust the velocity to maintain constant speed
                Vector3 reflection = Vector3.Reflect(rb.velocity.normalized, collision.contacts[0].normal);
                rb.velocity = reflection * speed;
            }
        }
    }

    protected override void OnTriggerEnter(Collider other)
    {
        
    }
}