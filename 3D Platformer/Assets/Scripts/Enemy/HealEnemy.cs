using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealEnemy : MonoBehaviour
{
    int healAmount = 50;
    public Transform[] respawnPoints;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            int ran = Random.Range(0, 3);
            GameObject enemy = other.gameObject;
            enemy.GetComponent<EnemyHealth>().HealEnemy(healAmount);
            transform.position = respawnPoints[ran].transform.position;
            
        }
    }
}
