using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleEnable : MonoBehaviour
{
    public GameObject ToToggle;
    public string togglerName;
    public bool StartEnabled = false;
    //public float timerLength = 10;
    bool isToggled;
    // Start is called before the first frame update
    void Awake()
    {
        Interactable.OnInteractAction += OnInteractHandler;
        ToToggle.SetActive(StartEnabled);
    }
    private void OnDestroy()
    {
        Interactable.OnInteractAction -= OnInteractHandler;
    }


    public void OnInteractHandler(string name, string parentName)
    {
        if (name != togglerName)
            return;
        if (StartEnabled)
        {
            Disable();
        }
        else
            Enable();
    }

    void Disable()
    {
        ToToggle.SetActive(false);
        isToggled = false;
    }

    private void Enable()
    {
        ToToggle.SetActive(true);
        isToggled = true;
    }
    // Update is called once per frame
    void Update()
    {

    }
}
