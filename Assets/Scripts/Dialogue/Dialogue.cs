using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    //public DialogueHolder dialogueHolder;
    public float textSpeed = 0.01f;
    public MenuManager menuManager;
    public DialogueTrigger dialogueTrigger;
    private string[] lines;
    public bool SceneHasMultipleSpeakers;

    private int index;
    // Start is called before the first frame update
    void Start()
    {
        dialogueText.text = string.Empty;
        //StartDialogue();
    }

    public void Click()
    {
        if (dialogueText.text == lines[index])
        {
            if (SceneHasMultipleSpeakers)
                dialogueTrigger.NextSpeaker();
            NextLine();
        }
        else
        {
            StopAllCoroutines();
            dialogueText.text = lines[index];
        }
    }

    public void SetText(string[] text)
    {
        lines = text;
    }

    public void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        dialogueText.text = string.Empty;
        int counter = 0;
        foreach (char c in lines[index].ToCharArray())
        {
            if (counter % 3 == 0)
            {
                SoundManager.Instance.PlayDialogueSFX();
            }

            dialogueText.text += c;
            yield return new WaitForSecondsRealtime(textSpeed);
            counter++;
        }
    }

    void NextLine()
    {
        SoundManager.Instance.PlayNextLine();
        dialogueText.text = string.Empty;
        if (index < lines.Length - 1)
        {
            index++;
            StartCoroutine(TypeLine());
        }
        else
        {
            index = 0;
            menuManager.CloseDialogue();
        }
    }

/*    [SerializeField] private SubClass[] myArray;

    public void SetValue(int index, SubClass subClass)
    {

        // Perform any validation checks here.
        myArray[index] = subClass;

    }

    public SubClass GetValue(int index)
    {

        // Perform any validation checks here.
        return myArray[index];

    }*/

}

/*[System.Serializable]
public class SubClass
{

    public int lines;
    public string text;

}
*/