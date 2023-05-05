using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenShop : MonoBehaviour
{
    public string togglerName;
    public GameObject shopUI;
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
        shopUI.SetActive(true);
        isShopping = true;
    }

    public void DoneShopping()
    {
        shopUI.SetActive(false);
        isShopping = false;
    }

}
