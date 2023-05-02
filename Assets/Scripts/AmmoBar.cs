using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoBar : MonoBehaviour
{
    //Slider for the Ammo Bar
    public Slider ammoSlider;

    //Sets the ammo of the player based on the integer parameter
    public void SetAmmo(int ammo)
    {
        //Slider of the ammo bar will be set to the value of ammo
        //This will set the ammo in game
        ammoSlider.value = ammo;
    }

    //Sets the maximum value of the ammo to the slider based on the integer parameter
    public void SetMaxAmmo(int ammo)
    {
        //Maximum value of the ammo bar slider is set
        ammoSlider.maxValue = ammo;

        //The ammo bar slider will be set to that same maximum value
        ammoSlider.value = ammo;
    }
}
