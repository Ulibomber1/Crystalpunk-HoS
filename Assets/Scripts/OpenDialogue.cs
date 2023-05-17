using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDialogue : MonoBehaviour
{
    public string togglerName;
    public MenuManager menuManager;
    private bool isTalking = false;
    // Start is called before the first frame update
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

    public void Talking(string name, string[] text)
    {
        menuManager.OpenDialogue(name, text);
        isTalking = true;
    }

    public void DoneTalking()
    {
        
        isTalking = false;
    }

}
