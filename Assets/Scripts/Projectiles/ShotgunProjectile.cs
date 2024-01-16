using UnityEngine;

public class ShotgunProjectile : Projectile
{
    public GameObject standardProjectilePrefab; // Assign the StandardProjectile prefab
    public Material shotgunProjectileMat;
    public float spreadAngle = 15f; // Angle for the spread of the shotgun

    public LayerMask wallLayer; // Set this in the Inspector to the "wall" layer.

    protected override void Start()
    {
        // Do not call base.Start() since we're overriding the behavior

        // Create the three projectiles
        CreateProjectile(transform.forward); // Central projectile
        CreateProjectile(Quaternion.Euler(0, -spreadAngle, 0) * transform.forward); // Left projectile
        CreateProjectile(Quaternion.Euler(0, spreadAngle, 0) * transform.forward); // Right projectile

        // Destroy the shotgun projectile itself
        Destroy(gameObject);
    }

    private void CreateProjectile(Vector3 direction)
    {
        if (standardProjectilePrefab != null)
        {
            GameObject projectileObject = Instantiate(standardProjectilePrefab, transform.position, Quaternion.LookRotation(direction));
            Projectile projectile = projectileObject.GetComponent<Projectile>();

            if (projectile != null)
            {
                
                projectile.parent = this.parent; // Set the parent variable
                projectile.lifetime = 1f; // Set a custom lifetime for each projectile
                projectile.speed = this.speed; // Example speed
                projectile.damage = this.damage;   // Example damage

                // Set the material of the projectile
                Renderer projectileRenderer = projectileObject.GetComponent<Renderer>();
                if (projectileRenderer != null && shotgunProjectileMat != null)
                {
                    projectileRenderer.material = shotgunProjectileMat;
                }
            }
        }
    }

    //protected override void OnTriggerEnter(Collider other)
    //{
    //    if (parent == 0)
    //    {
    //        InteractiveObject interactiveObject = other.gameObject.GetComponent<InteractiveObject>();
    //        if (interactiveObject != null)
    //        {
    //            interactiveObject.TakeDamage(damage);
    //            Destroy(gameObject);  // Optionally, destroy the bullet after hitting.
    //            Debug.Log("Object collision");
    //        }

    //        // Check for EnemyHealth component
    //        EnemyHealth enemyHealth = other.gameObject.GetComponent<EnemyHealth>();
    //        if (enemyHealth != null)
    //        {
    //            enemyHealth.TakeDamage(damage);
    //            Destroy(gameObject);  // Optionally, destroy the bullet after hitting.
    //            Debug.Log("Enemy collision");
    //            return; // Exit the method to avoid further checks.
    //        }
    //    }
    //    else if (parent == 1)
    //    {
    //        Debug.Log(other.gameObject.GetComponent<PlayerHealth>());
    //        PlayerHealth playerHealth = other.gameObject.GetComponent<PlayerHealth>();
    //        if (playerHealth != null)
    //        {
    //            playerHealth.TakeDamage(damage);
    //            Destroy(gameObject);  // Optionally, destroy the bullet after hitting.

    //            return; // Exit the method to avoid further checks.
    //        }
    //    }


    //    // If the collided object is on the "wall" layer
    //    if (wallLayer == (wallLayer | (1 << other.gameObject.layer)))
    //    {
    //        Destroy(gameObject);
    //    }
    //}
}
