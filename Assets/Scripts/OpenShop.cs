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
        Interactable.OnInteractAction += OnInteractHandler;
    }


    public void OnInteractHandler(string name)
    {
        if (name != togglerName)
            return;
        if (!isShopping)
        {
            Shopping();
        }
        else
        {
            DoneShopping();
        }


        Debug.Log("Dialogue " + isShopping);
    }

    public void Shopping()
    {
        menuManager.OpenShop();
    }

    public void DoneShopping()
    {
        menuManager.CloseShop();
    }

}
