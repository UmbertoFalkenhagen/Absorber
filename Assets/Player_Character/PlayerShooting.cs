using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public Transform firePoint; // Assign a point from where projectiles are spawned (tip of the gun/muzzle).

    public GameObject currentProjectileType = null;
    public int ammo = 0;
    public AmmoBar ammoBar;


    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }

        if (ammo > 0)
        {
            ammoBar.gameObject.SetActive(true);
        } else
        {
            ammoBar.gameObject.SetActive(false);
        }
        
        
    }

    void Shoot()
    {
        if (firePoint && currentProjectileType != null)
        {
            GameObject newProjectile = Instantiate(currentProjectileType, firePoint.position, firePoint.rotation);

            // Set the projectile's speed and damage (adjust these values as needed)
            Projectile projectileComponent = newProjectile.GetComponent<Projectile>();
            if (projectileComponent != null)
            {
                projectileComponent.parent = 0;
                projectileComponent.speed = 10.0f; // Example speed
                projectileComponent.damage = 1;   // Example damage
            }

            SoundManager.Instance.PlaySoundOnce("Shoot");
            ammo -= 1;
            if (ammo <= 0)
            {
                currentProjectileType = null;
            }
        } else
        {
            SoundManager.Instance.PlaySoundOnce("EmptyGun");
        }
    }
}