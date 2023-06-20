using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiDialogueTrigger : MonoBehaviour
{

/*    public DialogueInteractable npc1;
    public DialogueInteractable npc2;
    public DialogueInteractable npc3;
    public DialogueInteractable npc4;
    public MenuManager menuManager;
    public string togglerName;

    void Awake()
    {
        DialogueInteractable.OnDialogueEvent += OnInteractHandler;
    }

    void OnDestroy()
    {
        DialogueInteractable.OnDialogueEvent -= OnInteractHandler;
    }

    public void OnInteractHandler(string name, string parentName, string[] text)
    {
        if (name != togglerName)
            return;
        if (!isTalking)
        {
            Talking(parentName, text);
        }
        else
        {
            DoneTalking();
        }


        Debug.Log("Dialogue " + isTalking);
    }

    bool isTalking = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!isTalking)
            {
                isTalking = true;

            }
            


        }
    }

    public void Talking(string name, string[] text)
    {
        menuManager.OpenDialogue(name, text);
        isTalking = true;
    }

    public void DoneTalking()
    {

        isTalking = false;
    }*/
}
