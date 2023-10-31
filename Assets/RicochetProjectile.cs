using UnityEngine;

public class RicochetProjectile : Projectile
{
    public LayerMask wallLayer; // Set this in the Inspector to the "wall" layer.
    public int maxRicochets = 2; // Maximum number of ricochets before being destroyed.

    public int currentRicochetCount = 0;

    private void OnCollisionEnter(Collision collision)
    {
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
}