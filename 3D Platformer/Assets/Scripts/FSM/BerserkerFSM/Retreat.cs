using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Retreat : EnemyBaseFSM
{

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        enemyStateText.text = "RETREATING";
        enemy.GetComponent<EnemyAI>().aStar.SetActive(true);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.SetDestination(healthPickUp.transform.position);
        agent.speed = 10;

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy.GetComponent<EnemyAI>().aStar.SetActive(false);
        agent.speed = 5;
    }

}
