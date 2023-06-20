using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTrigger : MonoBehaviour
{
    [SerializeField] private string levelName;
    public MenuManager menuManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            menuManager.MoveToScene(levelName);
            //GameManager.Instance.ChangeScene(levelName);
        }
    }
}
