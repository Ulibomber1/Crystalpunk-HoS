using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interactable : MonoBehaviour
{

    public delegate void InteractActionHandler(string name);
    public static event InteractActionHandler OnInteractAction;

    private bool isInteractable = false;

    private void Awake()
    {
        PlayerController.OnInteraction += BroadcastToggle;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;
        isInteractable = true;
        Debug.Log("Enter Range");
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;
        isInteractable = false;
        Debug.Log("Exit Range");
    }


    public void BroadcastToggle()
    {
        if (!isInteractable)
            return;
        Debug.Log("Lever Pulled");
        OnInteractAction?.Invoke(gameObject.name);
    }
}
