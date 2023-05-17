using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenShop : MonoBehaviour
{
    public string togglerName;
    public GameObject shopUI;
    public MenuManager menuManager;
    private bool isShopping = false;
    // Start is called before the first frame update
    void Awake()
    {
        DialogueInteractable.OnDialogueEvent += OnInteractHandler;
    }

    void OnDestroy()
    {
        DialogueInteractable.OnDialogueEvent -= OnInteractHandler;
    }

    public void OnInteractHandler(string name, string parentName, string[]text)
    {
        if (name != togglerName)
            return;
        if (!isShopping)
        {
            Shopping(text);
        }
        else
        {
            DoneShopping();
        }


        Debug.Log("Dialogue " + isShopping);
    }

    public void Shopping(string[] text)
    {
        menuManager.OpenShop(text);
    }

    public void DoneShopping()
    {
        menuManager.CloseShop();
    }

}
