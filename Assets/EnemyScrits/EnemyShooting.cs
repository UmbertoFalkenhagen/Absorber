using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform shootPoint;
    public float shootCooldown = 2f; // Time between shots
    private float nextShootTime;

    private void Start()
    {
        // Initialize nextShootTime to start shooting immediately
        nextShootTime = Time.time;
    }

    private void Update()
    {
        // Check if it's time to shoot
        if (Time.time >= nextShootTime)
        {
            // Shoot projectile
            Shoot();

            // Update next shoot time
            nextShootTime = Time.time + shootCooldown;
        }
    }

    private void Shoot()
    {
        // Instantiate a new projectile at the shoot point
        GameObject newProjectile = Instantiate(projectilePrefab, shootPoint.position, shootPoint.rotation);

        // Set the projectile's speed and damage (adjust these values as needed)
        Projectile projectileComponent = newProjectile.GetComponent<Projectile>();
        if (projectileComponent != null)
        {
            projectileComponent.parent = 1;
            projectileComponent.speed = 10.0f; // Example speed
            projectileComponent.damage = 1;   // Example damage
        }
    }
}