using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThanksForPlaying : MonoBehaviour
{
    public MenuManager menuManager;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Cursor.lockState = CursorLockMode.None;
            menuManager.InGameSwitch("Thanks");
        }
    }
}
