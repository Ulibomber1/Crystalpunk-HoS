using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeUtility : MonoBehaviour
{
    public string togglerName;
    private MeshRenderer mr;
    // Start is called before the first frame update
    void Awake()
    {
        Interactable.OnInteractAction += OnInteractHandler;
        mr = gameObject.GetComponent<MeshRenderer>();
    }


    public void OnInteractHandler(string name)
    {
        if (name != togglerName)
            return;
        if(mr.enabled)
            mr.enabled = false;
        else
            mr.enabled = true;
        

        Debug.Log("Object " + mr.enabled);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
