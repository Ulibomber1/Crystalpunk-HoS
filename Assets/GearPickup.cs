using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearPickup : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameManager.gears += 1;
            Debug.Log(GameManager.gears + " gears collected");
        }
    }
}
