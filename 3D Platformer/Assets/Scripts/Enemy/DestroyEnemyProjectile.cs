using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEnemyProjectile : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, 1.5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Vector3 hitDirection = other.transform.position - transform.position;
            hitDirection = hitDirection.normalized; //restricts distance

            FindObjectOfType<HealthManager>().DamagePlayer(1, hitDirection);
            Destroy(gameObject);
        }
    }
}
