using UnityEngine;

public class AutoDestroyParticle : MonoBehaviour
{
    private ParticleSystem particleSystem;
    private float previousTime = 0f;
    private int loopCount = 0;

    void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        // Check if the time has reset, indicating a loop
        if (particleSystem.time < previousTime)
        {
            loopCount++;
            Debug.Log("Particle system has looped " + loopCount + " times.");
        }

        if (loopCount >= 2)
        {
            Destroy(gameObject);
        }

        previousTime = particleSystem.time;
    }
}
