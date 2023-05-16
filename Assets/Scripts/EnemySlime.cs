using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemySlime : MonoBehaviour
{
    public int HP = 5;
    public Animator animator;
    public Slider healthBar;

    void update()
    {
        healthBar.value = HP;
    }

    public void E_TakeDamage(int damageAmount)
    {
        HP -= damageAmount;
        if (HP <= 0)
        {
            animator.SetTrigger("die");
            healthBar.value = HP;
            GetComponent<Collider>().enabled = false;
            Destroy(this.gameObject, 1);
            Debug.Log("slime died");
        }
        else
        {
            animator.SetTrigger("damage");
            Debug.Log("slime damaged");
            healthBar.value = HP;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("playerBullet"))
        {
            E_TakeDamage(2);
            Debug.Log("damaged slime");
            Destroy(gameObject);
        }

    }
}
