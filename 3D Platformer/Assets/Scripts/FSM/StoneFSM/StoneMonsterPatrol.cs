using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoneMonsterPatrol : StoneMonsterFSM
{
    private float waitTime = 0;
    private int randomSpot;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        enemyStateText.text = "PATROL";
        waitTime = enemy.GetComponent<StoneMonsterAI>().startWaitTime;
        randomSpot = Random.Range(0, enemy.GetComponent<StoneMonsterAI>().moveSpots.Length);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, enemy.GetComponent<StoneMonsterAI>().moveSpots[randomSpot].position,
            enemy.GetComponent<StoneMonsterAI>().moveSpeed * Time.deltaTime);

        if (Vector3.Distance(enemy.transform.position, enemy.GetComponent<StoneMonsterAI>().moveSpots[randomSpot].position) < 0.2f)
        {
            if (waitTime <= 0)
            {
                randomSpot = Random.Range(0, enemy.GetComponent<StoneMonsterAI>().moveSpots.Length);
                waitTime = enemy.GetComponent<StoneMonsterAI>().startWaitTime;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }

        }
    }
}
