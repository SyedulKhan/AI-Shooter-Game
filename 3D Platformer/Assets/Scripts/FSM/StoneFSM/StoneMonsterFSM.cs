using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoneMonsterFSM : StateMachineBehaviour
{
    public GameObject enemy;
    public GameObject player;
    public Text enemyStateText;

    public float speed = 2.0f;
    public float rotationSpeed = 1.0f;
    public float accuracy = 3.0f;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy = animator.gameObject;
        player = enemy.GetComponent<StoneMonsterAI>().GetPlayer();
        enemyStateText = enemy.GetComponent<StoneMonsterAI>().EnemyState();
    }
}
