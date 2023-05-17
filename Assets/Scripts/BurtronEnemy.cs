using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class BurtronEnemy : MonoBehaviour
{
    public int HP = 100;
    public Slider healthBar;
    public bool bossActivated = false;
    public enemySpawner spawner;
    //public GameObject interactable

    void update()
    {
        healthBar.value = HP;
    }

    public void E_TakeDamage(int damageAmount)
    {
        HP -= damageAmount;
        if (HP <= 0)
        {
            //enable gameobject crystal_interactable
            bossActivated = false;
            spawner.defeatBoss();
            healthBar.value = HP;
            GetComponent<Collider>().enabled = false;
            this.gameObject.SetActive(false);
            Debug.Log("Burtron died");

            
        }
        else
        {
            Debug.Log("damaged Burtron!");
            healthBar.value = HP;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("playerBullet"))
        {
            E_TakeDamage(1);
            Debug.Log("damaged Burtron!!");
            Destroy(other.gameObject);
        }
    }
}
