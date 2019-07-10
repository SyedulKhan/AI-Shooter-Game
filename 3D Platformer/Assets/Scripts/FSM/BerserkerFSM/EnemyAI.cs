using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyAI : MonoBehaviour
{
    Animator anim;
    public GameObject player;
    public GameObject projectile;
    public GameObject bomb;
    public GameObject fire1;
    public GameObject fire2;
    public GameObject fire3;
    public GameObject playerBullet;
    public float health;
    public Text enemyStateText;
    public GameObject aStar;
    bool dodge;
    public Rigidbody rb;

    public GameObject GetPlayer()
    {
        return player;
    }

    public Text EnemyState()
    {
        return enemyStateText;
    }

    void Attack()
    {
        int ran = Random.Range(1, 10);

        if (ran == 1)
        {
            GameObject proj3 = Instantiate(bomb, fire3.transform.position, fire3.transform.rotation);
            proj3.GetComponent<Rigidbody>().AddForce(fire3.transform.forward * 500);
        }
        else
        {
            GameObject proj1 = Instantiate(projectile, fire1.transform.position, fire1.transform.rotation);
            proj1.GetComponent<Rigidbody>().AddForce(fire1.transform.forward * 500);
            GameObject proj2 = Instantiate(projectile, fire2.transform.position, fire2.transform.rotation);
            proj2.GetComponent<Rigidbody>().AddForce(fire2.transform.forward * 500);
        }
    }

    public void StartAttacking()
    {
        InvokeRepeating("Attack", 0.5f, 0.5f);
    }

    public void StopAttacking()
    {
        CancelInvoke("Attack");
    }

    public void DodgeBullet()
    {
        //playerBullet = GameObject.FindGameObjectWithTag("Bullet");

        //if (playerBullet != null)
        //{
        //    rb.AddForce(transform.up * 500);
        //}
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
        //playerBullet = GameObject.FindGameObjectWithTag("Bullet");
        //anim.SetFloat("bulletDistance", Vector3.Distance(transform.position, playerBullet.transform.position));
    }
}
