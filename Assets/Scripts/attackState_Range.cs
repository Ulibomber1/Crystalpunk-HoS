using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class attackState_Range : StateMachineBehaviour
{
    [SerializeField] private float timer;
    private float bulletTime;
    NavMeshAgent agent;
    Transform player;

    public float enemyBulletSpeed;
    bool alreadyAttacked;
    public GameObject projectile;
    //public float tooCloseAttack;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = animator.GetComponent<NavMeshAgent>();
        alreadyAttacked = false;
        bulletTime = timer;
    }


    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Make sure enemy doesn't move
        agent.SetDestination(animator.transform.position);
        bulletTime -= Time.deltaTime;
        //Debug.Log(bulletTime);
        animator.transform.LookAt(player);
        if (!alreadyAttacked)
        {
            ///Attack code here
            GameObject bulletObj = Instantiate(projectile, animator.transform.position, animator.transform.rotation) as GameObject;
            Rigidbody bulletRig = bulletObj.GetComponent<Rigidbody>();
            bulletRig.AddForce(bulletRig.transform.forward * enemyBulletSpeed);
            Destroy(bulletObj, 3f);
            ///End of attack code
            alreadyAttacked = true;     
        }
        
        if (bulletTime <= 0)
        {
            alreadyAttacked = false;
            bulletTime = timer;
        }

        //float distance = Vector3.Distance(player.position, animator.transform.position);
        //if (distance > tooCloseAttack)
        //    animator.SetBool("isAttacking", false);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.SetDestination(animator.transform.position);
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
