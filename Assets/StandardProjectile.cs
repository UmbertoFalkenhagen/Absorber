using UnityEngine;

public class StandardProjectile : Projectile
{
    public LayerMask wallLayer; // Set this in the Inspector to the "wall" layer.

    protected override void OnTriggerEnter(Collider other)
    {
        // If the collided object is on the "wall" layer
        if (wallLayer == (wallLayer | (1 << other.gameObject.layer)))
        {
            Destroy(gameObject);
        }
    }
}