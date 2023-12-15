using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CooldownBar : HealthBar
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (slider.value == slider.maxValue)
        {
            slider.image.color = Color.green;
        }
        else
        {
            slider.image.color = Color.red;
        }
    }
}
