using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class BubbleFSM : StateMachineBehaviour
{
    public GameObject enemy;
    public GameObject player;
    public Text enemyStateText;
    public NavMeshAgent agent;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy = animator.gameObject;
        player = enemy.GetComponent<BubbleAI>().GetPlayer();
        enemyStateText = enemy.GetComponent<BubbleAI>().EnemyState();
        agent = animator.GetComponent<NavMeshAgent>();
    }
}
