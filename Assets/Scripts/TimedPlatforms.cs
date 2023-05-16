using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedPlatforms : MonoBehaviour
{
    public GameObject platformHolder;
    public string togglerName;
    public float timerLength = 10;
    bool isToggled;
    // Start is called before the first frame update
    void Awake()
    {
        Interactable.OnInteractAction += OnInteractHandler;
        platformHolder.SetActive(false);
    }

    void OnDestroy()
    {
        Interactable.OnInteractAction -= OnInteractHandler;
    }


    public void OnInteractHandler(string name, string parentName)
    {
        if (name != togglerName)
            return;
        if(!isToggled)
        {
            platformHolder.SetActive(true);
            isToggled = true;
            Invoke("DespawnPlatform", timerLength);
        }
        else
            Debug.Log("Platforms already enabled!");
    }

    void DespawnPlatform()
    {
        platformHolder.SetActive(false);
        isToggled = false;
    }
    // Update is called once per frame
    void Update()
    {

    }
}
