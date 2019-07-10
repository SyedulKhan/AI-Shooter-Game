using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleAssistOthers : BubbleFSM
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        enemyStateText.text = "ALERTED";
        enemy.GetComponent<BubbleAI>().aStar.SetActive(true);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        TrackDownTarget(5);
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy.GetComponent<BubbleAI>().aStar.SetActive(false);
    }

    private void TrackDownTarget(float scale)
    {
        Vector3 direction = (player.transform.position - enemy.transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        enemy.transform.rotation = Quaternion.Slerp(enemy.transform.rotation, lookRotation, Time.deltaTime * 5f);
        enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, player.transform.position, (enemy.GetComponent<BubbleAI>().speed + scale) * Time.deltaTime);
    }
}
