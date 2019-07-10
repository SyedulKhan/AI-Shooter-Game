using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : EnemyBaseFSM
{
    GameObject[] points;
    int currentPoint;

    private void Awake()
    {
        points = GameObject.FindGameObjectsWithTag("Point");
    }

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        enemyStateText.text = "PATROL";
        currentPoint = Random.Range(0, points.Length -1);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (points.Length == 0)
        {
            return;
        }

        if (Vector3.Distance(points[currentPoint].transform.position, enemy.transform.position) < accuracy)
        {
            currentPoint++;
            if (currentPoint >= points.Length)
            {
                currentPoint = 0;
            }
        }

        agent.SetDestination(points[currentPoint].transform.position);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

}
