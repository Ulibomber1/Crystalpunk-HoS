using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss_trigger : MonoBehaviour
{
    public BurtronEnemy burtronEnemy;
    public enemySpawner spawner;
    public GameObject bossDoor;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            bossDoor.GetComponent<MeshRenderer>().enabled = true;
            bossDoor.GetComponent<MeshCollider>().enabled = true;
            Debug.Log("Boss Fight");
            burtronEnemy.bossActivated = true;
            spawner.triggerBoss();
            GetComponent<Collider>().enabled = false;
        }
    }
}
