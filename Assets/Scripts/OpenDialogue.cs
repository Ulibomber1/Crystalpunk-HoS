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
            dialogueBox.SetActive(true);
            isTalking = true;
        }
        else
        {
            dialogueBox.SetActive(false);
            isTalking = false;
        }


        Debug.Log("Dialogue" + isTalking);
    }
    // Update is called once per frame
    void Update()
    {

    }
}
