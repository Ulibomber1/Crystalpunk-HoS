using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyMiniBoss : MonoBehaviour
{
    public int HP = 20;
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
            Debug.Log("miniboss died");
        }
        else
        {
            animator.SetTrigger("damage");
            healthBar.value = HP;
            Debug.Log("damaged miniboss");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("playerBullet"))
        {
            E_TakeDamage(2);
            Debug.Log("damaged miniboss");
            //Destroy(gameObject);
        }

    }
}
