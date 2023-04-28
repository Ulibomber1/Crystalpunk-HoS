using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public int HP = 10;
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
        }
        else
        {
            animator.SetTrigger("damage");
        }
    }
}
