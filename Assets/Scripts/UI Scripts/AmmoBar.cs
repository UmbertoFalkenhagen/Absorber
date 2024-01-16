using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoBar : MonoBehaviour
{
    public enum ProjectileType { Standard, Shotgun, Ricochet }

    public ProjectileType projectileIcon;
    public Slider slider;
    public PlayerShooting playerShooting;

    public List<Sprite> icons;
    public Image activeIcon;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        SetAmmo(playerShooting.ammo);
        if (playerShooting.ammo > 0)
        {
            this.gameObject.SetActive(true);
        } else
        {
            this.gameObject.SetActive(false);
        }

        if (slider.value >= slider.maxValue / 2)
        {
            slider.image.color = Color.green;
        }
        else if (slider.value < slider.maxValue / 2 && slider.value >= slider.maxValue / 4)
        {
            slider.image.color = Color.yellow;
        }
        else if (slider.value < slider.maxValue / 4)
        {
            slider.image.color = Color.red;
        }

        if (projectileIcon == ProjectileType.Standard)
        {
            activeIcon.sprite = icons[0];
        } else if (projectileIcon == ProjectileType.Shotgun)
        {
            activeIcon.sprite = icons[1];
        } else if (projectileIcon == ProjectileType.Ricochet)
        {
            activeIcon.sprite = icons[2];
        }
    }

    public void SetAmmo(float ammo)
    {
        slider.value = ammo;
    }

    public void SetMaxAmmo(float ammo)
    {
        slider.maxValue = ammo;
    }
}
