using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoneMonsterAI : MonoBehaviour
{
    Animator anim;
    public GameObject player;
    public GameObject projectile;
    public GameObject fire;
    public float health;
    public Text enemyStateText;
    public float moveSpeed;
    public float startWaitTime;
    public Transform[] moveSpots;

    public GameObject GetPlayer()
    {
        return player;
    }

    public Text EnemyState()
    {
        return enemyStateText;
    }

    public void StartAttacking()
    {
        InvokeRepeating("Attack", 1f, 1f);
    }

    public void StopAttacking()
    {
        CancelInvoke("Attack");
    }

    void Attack()
    {
        GameObject proj1 = Instantiate(projectile, fire.transform.position, fire.transform.rotation);
        proj1.GetComponent<Rigidbody>().AddForce(fire.transform.forward * 500);
    }

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        health = GetComponent<EnemyHealth>().enemyHealth;
        anim.SetFloat("distance", Vector3.Distance(transform.position, player.transform.position));
        anim.SetFloat("health", health);
    }
}
