using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class BubbleAI : MonoBehaviour
{
    Animator anim;
    public float speed;
    public GameObject bombEffect;
    public static bool alertEveryone;
    public GameObject player;
    public float health;
    public Text enemyStateText;
    public GameObject aStar;

    public GameObject GetPlayer()
    {
        return player;
    }

    public Text EnemyState()
    {
        return enemyStateText;
    }

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        alertEveryone = false;
    }

    // Update is called once per frame
    void Update()
    {
        health = GetComponent<EnemyHealth>().enemyHealth;
        anim.SetFloat("distance", Vector3.Distance(transform.position, player.transform.position));
        anim.SetBool("alerted", alertEveryone);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Vector3 hitDirection = other.transform.position - transform.position;
            hitDirection = hitDirection.normalized; //restricts distance

            FindObjectOfType<HealthManager>().DamagePlayer(5, hitDirection);
            Instantiate(bombEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
