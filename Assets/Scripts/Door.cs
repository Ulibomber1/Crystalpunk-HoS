using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public string togglerName;
    private bool isOpen = false;

    // Start is called before the first frame update
    void Awake()
    {
        Interactable.OnInteractAction += OnInteractHandler;
    }


    public void OnInteractHandler(string name)
    {
        if (name != togglerName)
            return;
        if (!isOpen)
        {
            //transform.Rotate(0f, 90f, 0f);
            transform.Translate(0f, 2.5f, 0f);
            isOpen = true;
        }
        else
        {
            //transform.Rotate(0f, -90f, 0f);
            transform.Translate(0f, -2.5f, 0f);
            isOpen = false;
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}

