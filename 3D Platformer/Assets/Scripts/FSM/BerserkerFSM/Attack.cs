using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : EnemyBaseFSM
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        enemy.GetComponent<EnemyAI>().StartAttacking();
        enemyStateText.text = "ATTACK";
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //// Find the distance between weapon and Player so we can anticipate where Player will be
        //var distance = Vector3.Distance(player.transform.position, enemy.transform.position);

        //// Divide distance by weaponSpeed to see how long it will take to get from Enemy to Player
        //var travelTime = distance / (Mathf.Abs(5) + 10);

        //// Multiply the amount of time by the speed the Player moves at
        //var futurePosition = travelTime * 10;

        //// Assign the new location that we want to point the weapons towards
        //var newLocation = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z + futurePosition);

        //// Look at and dampen the rotation
        //var rotation = Quaternion.LookRotation(newLocation - enemy.transform.position);
        //enemy.transform.rotation = Quaternion.Slerp(enemy.transform.rotation, rotation, Time.deltaTime * 10);
        enemy.transform.LookAt(player.transform.position);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy.GetComponent<EnemyAI>().StopAttacking();
    }
}
