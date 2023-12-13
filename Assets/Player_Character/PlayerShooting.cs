using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public Transform firePoint; // Assign a point from where projectiles are spawned (tip of the gun/muzzle).
    public List<GameObject> projectilePrefabs; // List of different projectile prefabs.
    private int currentProjectileIndex = 0;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }

        // For testing, use number keys to switch projectiles
        for (int i = 0; i < projectilePrefabs.Count; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                currentProjectileIndex = i;
            }
        }
    }

    void Shoot()
    {
        if (firePoint && projectilePrefabs.Count > 0)
        {
            Instantiate(projectilePrefabs[currentProjectileIndex], firePoint.position, firePoint.rotation);
        }
    }
}