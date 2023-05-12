using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AttackState : StateMachineBehaviour
{
    Transform player;
    PlayerController playerC;
    NavMeshAgent agent;
    [SerializeField] private float timer = 2;
    private float attackTime;
    bool alreadyAttacked;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerC = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        alreadyAttacked = false;
        attackTime = timer;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.transform.LookAt(player);
        if (!alreadyAttacked)
        {
            playerC.PlayerDamage(1);
            Debug.Log("player damaged by 1!");
            alreadyAttacked = true;
        }
        attackTime -= Time.deltaTime;
        if (attackTime == 0)
        {
            alreadyAttacked = false;
            attackTime = timer;
        }
        //player goes out of range
        float distance = Vector3.Distance(player.position, animator.transform.position);
        if (distance > 1f)
            animator.SetBool("isAttacking", false);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
