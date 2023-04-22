using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class LeverController : MonoBehaviour
{

    public delegate void LeverActionHandler(string name);
    public static event LeverActionHandler OnLeverAction;

    private bool isInteractable = false;

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


    public void OnInteract(InputValue context)
    {
        if (!isInteractable)
            return;
        Debug.Log("Lever Pulled");
        OnLeverAction?.Invoke(gameObject.name);
    }
}
