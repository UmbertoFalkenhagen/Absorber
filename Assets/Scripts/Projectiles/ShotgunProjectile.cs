using UnityEngine;

public class ShotgunProjectile : Projectile
{
    public GameObject standardProjectilePrefab; // Assign the StandardProjectile prefab
    public Material shotgunProjectileMat;
    public float spreadAngle = 15f; // Angle for the spread of the shotgun

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

                // Set the material of the projectile
                Renderer projectileRenderer = projectileObject.GetComponent<Renderer>();
                if (projectileRenderer != null && shotgunProjectileMat != null)
                {
                    projectileRenderer.material = shotgunProjectileMat;
                }
            }
        }
    }
}
