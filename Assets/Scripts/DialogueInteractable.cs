using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueInteractable : Interactable
{
    public delegate void DialogueStringHandler(string name, string parentName, string[] text);
    public static event DialogueStringHandler OnDialogueEvent;

    [SerializeField] DialogueHolder dialogueHolder;

    protected override void BroadcastToggle()
    {
        if (!isInteractable)
            return;
        Debug.Log("Dialogue Dialoged");
        OnDialogueEvent?.Invoke(gameObject.name, this.transform.parent.gameObject.name, dialogueHolder.lines);
    }
}
