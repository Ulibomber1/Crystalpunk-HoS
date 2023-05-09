using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolStateBug : StateMachineBehaviour
{
    float timer;
    public string waypointname;
    List<Transform> wayPoints = new List<Transform>();
    NavMeshAgent agent;
    Transform player;
    float chaseRange = 8;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = animator.GetComponent<NavMeshAgent>();
        timer = 0;
        GameObject go = GameObject.FindGameObjectWithTag(waypointname);
        foreach (Transform t in go.transform)
            wayPoints.Add(t);

        agent.SetDestination(wayPoints[Random.Range(0, wayPoints.Count)].position);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (agent.remainingDistance <= agent.stoppingDistance)
            agent.SetDestination(wayPoints[Random.Range(0, wayPoints.Count)].position);
        timer += Time.deltaTime;
        if (timer > Random.Range(10, 30))
            animator.SetBool("isPatrolling", false);
        float distance = Vector3.Distance(player.position, animator.transform.position);
        if (distance < chaseRange)
            animator.SetBool("isChasing", true);

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.SetDestination(agent.transform.position);
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
