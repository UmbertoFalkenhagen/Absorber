using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    public float speed = 10.0f;
    public int damage = 10;

    public float lifetime = 5.0f; // Time (in seconds) before the projectile is automatically destroyed.

    public int parent; // 0 = instantiated by player, 1 = instantiated by enemy

    protected Rigidbody rb;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = transform.forward * speed;
        }

        // Schedule the projectile to be destroyed after its lifetime expires.
        Destroy(gameObject, lifetime);
    }


    protected virtual void OnTriggerEnter(Collider other)
    {
        // Handle hit logic
        // For example, you might deal damage to enemies, or play effects

        Destroy(gameObject); // Destroy the projectile upon collision. This can be replaced with pooling later.
    }

}
