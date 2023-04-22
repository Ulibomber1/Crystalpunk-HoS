using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This code may be deprecated (behavior moved to PlayerController)
public class GearPickup : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerInteract"))
            return;

        if (other.gameObject.CompareTag("Player"))
        {
            //gameObject.SetActive(false);

            //Debug.Log(PlayerController.gears + " gears collected");
        }
    }
}
