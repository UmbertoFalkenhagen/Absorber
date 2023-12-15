using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (slider.value >= slider.maxValue/2)
        {
            slider.image.color = Color.green;
        } else if (slider.value < slider.maxValue/2 && slider.value >= slider.maxValue/4)
        {
            slider.image.color = Color.yellow;
        } else if (slider.value < slider.maxValue/4)
        {
            slider.image.color = Color.red;
        }
    }

    public void SetHealth(float health)
    {
        slider.value = health;
    }

    public void SetMaxHealth(float health)
    {
        slider.maxValue = health;
    }
}
