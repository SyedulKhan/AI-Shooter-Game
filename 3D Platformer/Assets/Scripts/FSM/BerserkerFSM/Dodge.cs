using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dodge : EnemyBaseFSM
{
    // OnStateEnter is called before OnStateEnter is called on any state inside this state machine
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        enemyStateText.text = "DODGE";
    }

    // OnStateUpdate is called before OnStateUpdate is called on any state inside this state machine
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        int dodgeProb = Random.Range(1, 10);
        if (dodgeProb == 1)
        {
            enemy.GetComponent<EnemyAI>().DodgeBullet();
        }
    }
}
