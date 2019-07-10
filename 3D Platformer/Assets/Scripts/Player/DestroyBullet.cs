using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBullet : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {

        Debug.Log(collision.transform.name);

        if (collision.transform.tag == "Enemy")
        {
            GameObject target = collision.gameObject;
            target.GetComponent<EnemyHealth>().DamageEnemy(5);
            gameObject.SetActive(false);
        }
    }


}
