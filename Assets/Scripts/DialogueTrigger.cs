using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    private bool hasTriggered = false;
    public MenuManager menuManager;
    public string[] parentName;
    [TextArea(3, 10)]
    public string[] text;
    private int speaker = 0;
    public int numOfSpeakers;

    private void Awake()
    {
        name = parentName[speaker];
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Talking(name, text);
            //Time.timeScale = 0;
        }
    }

    public void NextSpeaker()
    {
        if (speaker < numOfSpeakers)
        {
            speaker++;
            name = parentName[speaker];
            Talking(name, text);
        }
        else
        {
            //Time.timeScale = 1;
        }
    }

    public void Talking(string name, string[] text)
    {
        if (!hasTriggered)
        {
            menuManager.OpenDialogue(name, text);
            hasTriggered = true;
        }
        else
        {
            menuManager.dialogueName.SetText(name);
        }
        //isTalking = true;
    }

/*    public void DoneTalking()
    {

        isTalking = false;
    }*/
}
