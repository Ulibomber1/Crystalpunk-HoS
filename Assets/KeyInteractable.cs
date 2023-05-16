using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyInteractable : Interactable
{
    public delegate void KeyActionHandler(string name);
    public static event KeyActionHandler OnKeyAction;

    protected override void BroadcastToggle()
    {
        if (!isInteractable)
            return;
        OnKeyAction?.Invoke(gameObject.name);
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;
    }
}
