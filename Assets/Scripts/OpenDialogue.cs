using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDialogue : MonoBehaviour
{
    public string togglerName;
    public GameObject dialogueBox;
    private bool isTalking = false;
    // Start is called before the first frame update
    void Awake()
    {
        Interactable.OnInteractAction += OnInteractHandler;
    }


    public void OnInteractHandler(string name)
    {
        if (name != togglerName)
            return;
        if (!isTalking)
        {
            Talking();
        }
        else
        {
            DoneTalking();
        }


        Debug.Log("Dialogue " + isTalking);
    }

    public void Talking()
    {
        dialogueBox.SetActive(true);
        isTalking = true;
    }

    public void DoneTalking()
    {
        dialogueBox.SetActive(false);
        isTalking = false;
    }

}