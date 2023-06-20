using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionHandler : MonoBehaviour
{
    [SerializeField] protected string togglerName;
    private MeshRenderer mr;
    private MeshCollider mc;
    // Start is called before the first frame update
    protected virtual void Awake()
    {
        Interactable.OnInteractAction += OnInteractHandler;
        mr = gameObject.GetComponent<MeshRenderer>();
        mc = gameObject.GetComponent<MeshCollider>();
    }

    protected virtual void OnDestroy()
    {
        Interactable.OnInteractAction -= OnInteractHandler;
    }

    private void OnInteractHandler(string name, string parentName)
    {
        if (name != togglerName)
            return;
        if (mr.enabled)
        {
            mr.enabled = false;
            mc.enabled = false;
        }
        else
        {
            mr.enabled = true;
            mc.enabled = true;
        }
           
        

        Debug.Log("Object " + mr.enabled);
    }
}
