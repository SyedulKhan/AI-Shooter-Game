using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public float enemyHealth;
    public float maxHealth;

    public GameObject healthBarUI;
    public Slider slider;

    public GameObject deathEffect;

    void Start()
    {
        enemyHealth = maxHealth;
        slider.value = CalculateHealth();
    }

    void Update()
    {
        slider.value = CalculateHealth();

        if (enemyHealth <= maxHealth)
        {
            healthBarUI.SetActive(true);
        }

        if (enemyHealth <= 0)
        {
            Instantiate(deathEffect, transform.position, transform.rotation);
            BubbleAI.alertEveryone = false;
            Destroy(gameObject);
        }

        if (enemyHealth > maxHealth)
        {
            enemyHealth = maxHealth;
        }
    }

    float CalculateHealth()
    {
        return enemyHealth / maxHealth; 
    }

    public void DamageEnemy(float damage)
    {
        enemyHealth -= damage;
    }

    public void HealEnemy(int healAmount)
    {
        enemyHealth += healAmount;

        if (enemyHealth > maxHealth)
        {
            enemyHealth = maxHealth;
        }
    }
}
