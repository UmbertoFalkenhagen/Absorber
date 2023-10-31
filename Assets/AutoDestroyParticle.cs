using UnityEngine;

public class AutoDestroyParticle : MonoBehaviour
{
    private ParticleSystem ps;
    public int currentLoopCount;

    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        
        
    }

    void Update()
    {
        
            currentLoopCount = Mathf.FloorToInt(ps.time / ps.main.duration);
        Debug.Log(ps.time);
            if (currentLoopCount >= 1 || (0.9f < ps.time && ps.time < 1))
            {
                Destroy(this);
            }
        
        //if (!ps.IsAlive()) // Check if the particle system has finished playing
        //{
             // Destroy the GameObject
        //}
    }
}
