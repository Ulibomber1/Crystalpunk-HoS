using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteractionHandler : InteractionHandler
{
    private bool isInteractable = false;
    private bool keyReceived = false;

    protected override void Awake()
    {
        KeyInteractable.OnKeyAction += OnKeyReceived;
        PlayerController.OnInteraction += OpenDoor;
    }
    protected override void OnDestroy()
    {
        KeyInteractable.OnKeyAction -= OnKeyReceived;
        PlayerController.OnInteraction -= OpenDoor;
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
    private void OnKeyReceived(string name)
    {
        if (name != togglerName)
            return;
        keyReceived = true;
    }
    private void OpenDoor()
    {
        if (!keyReceived || !isInteractable)
            return;
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<MeshCollider>().enabled = false;
        GetComponent<BoxCollider>().enabled = false;
        Debug.Log("Door Opened!");
    }
}
