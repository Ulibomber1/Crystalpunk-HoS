using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBullet : MonoBehaviour
{
    PlayerController playerC;
    // Start is called before the first frame update
    void Start()
    {
        playerC = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerC.PlayerDamage(1);
            Destroy(this.gameObject);
            Debug.Log("damaged player!"); 
           
        }
    }
}
