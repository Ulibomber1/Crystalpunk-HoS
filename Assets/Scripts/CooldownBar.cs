using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CooldownBar : MonoBehaviour
{
    //Slider for the Cooldown Bar
    public Slider cooldownSlider;

    //Sets the Cooldown of the player based on the integer parameter
    public void SetCooldown(int cooldown)
    {
        //Slider of the cooldown bar will be set to the value of cooldown
        //This will set the cooldown in game
        cooldownSlider.value = cooldown;
    }

    //Sets the maximum value of the cooldown to the slider based on the integer parameter
    public void SetMaxCooldown(int cooldown)
    {
        //Maximum value of the cooldown bar slider is set
        cooldownSlider.maxValue = cooldown;

        //The cooldown bar slider will be set to that same maximum value
        cooldownSlider.value = cooldown;
    }
}
