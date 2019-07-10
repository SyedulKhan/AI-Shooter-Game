using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class EnemyBaseFSM : StateMachineBehaviour
{
    public GameObject enemy;
    public GameObject player;
    public GameObject healthPickUp;
    public Text enemyStateText;
    public NavMeshAgent agent;
    public Rigidbody rb;

    public float speed = 2.0f;
    public float rotationSpeed = 1.0f;
    public float accuracy = 3.0f;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemy = animator.gameObject;
        player = enemy.GetComponent<EnemyAI>().GetPlayer();
        healthPickUp = FindObjectOfType<HealEnemy>().gameObject;
        enemyStateText = enemy.GetComponent<EnemyAI>().EnemyState();
        agent = animator.GetComponent<NavMeshAgent>();
        rb = enemy.GetComponent<Rigidbody>();
    }
}
