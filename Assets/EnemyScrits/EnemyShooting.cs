using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public enum ProjectileType { Standard, Shotgun, Ricochet }

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

    private ProjectileType selectedProjectileType;

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
            selectedProjectileType = ProjectileType.Standard;
        }
        else if (randomValue < projectileProbabilities.standardProbability + projectileProbabilities.shotgunProbability)
        {
            selectedProjectileType = ProjectileType.Shotgun;
        }
        else
        {
            selectedProjectileType = ProjectileType.Ricochet;
        }
    }

    private void Shoot()
    {
        GameObject projectilePrefab;

        switch (selectedProjectileType)
        {
            case ProjectileType.Shotgun:
                projectilePrefab = shotgunProjectilePrefab;
                break;
            case ProjectileType.Ricochet:
                projectilePrefab = ricochetProjectilePrefab;
                break;
            default:
                projectilePrefab = standardProjectilePrefab;
                break;
        }

        Instantiate(projectilePrefab, shootPoint.position, shootPoint.rotation);
    }
}