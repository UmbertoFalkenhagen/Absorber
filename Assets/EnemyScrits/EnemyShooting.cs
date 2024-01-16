using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    //public enum ProjectileType { Standard, Shotgun, Ricochet }

    [System.Serializable]
    public struct ProjectileProbability
    {
        public float standardProbability;
        public float shotgunProbability;
        public float ricochetProbability;
    }

    public GameObject standardProjectilePrefab;
    public GameObject shotgunProjectilePrefab;
    public GameObject ricochetProjectilePrefab;
    public Transform shootPoint;
    public float shootCooldown = 2f; // Time between shots
    private float nextShootTime;

    public ProjectileProbability projectileProbabilities;

    public GameObject selectedProjectileType;

    private void Start()
    {
        nextShootTime = Time.time;
        SelectRandomProjectileType();
    }

    private void Update()
    {
        if (Time.time >= nextShootTime)
        {
            Shoot();
            nextShootTime = Time.time + shootCooldown;
        }
    }

    private void SelectRandomProjectileType()
    {
        float totalProbability = projectileProbabilities.standardProbability +
                                 projectileProbabilities.shotgunProbability +
                                 projectileProbabilities.ricochetProbability;

        float randomValue = Random.Range(0, totalProbability);

        if (randomValue < projectileProbabilities.standardProbability)
        {
            selectedProjectileType = standardProjectilePrefab;
        }
        else if (randomValue < projectileProbabilities.standardProbability + projectileProbabilities.shotgunProbability)
        {
            selectedProjectileType = shotgunProjectilePrefab;
        }
        else
        {
            selectedProjectileType = ricochetProjectilePrefab;
        }
    }

    private void Shoot()
    {

        GameObject newProjectile = Instantiate(selectedProjectileType, shootPoint.position, shootPoint.rotation);

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