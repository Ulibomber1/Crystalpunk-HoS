using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    //Slider for the Health Bar
    public Slider healthSlider;

    //Sets the health of the player based on the integer parameter
    public void SetHealth(int health)
    {
        //Slider of the health bar will be set to the value of health
        //This will set the health in game
        healthSlider.value = health;
    }

    //Sets the maximum value of the health to the slider based on the integer parameter
    public void SetMaxHealth(int health)
    {
        //Maximum value of the health bar slider is set
        healthSlider.maxValue = health;

        //The health bar slider will be set to that same maximum value
        healthSlider.value = health;
    }
}
