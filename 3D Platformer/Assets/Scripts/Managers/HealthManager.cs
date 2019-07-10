using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour {

    public int currentHealth;
    public int maxHealth;
    public PlayerController player;

    public int currentLives;

    public float invincibilityLength;
    private float invincibilityCounter;

    public Renderer playerRenderer;
    public float flashLength = 0.1f;
    private float flashCounter;

    private bool isRespawning;
    private Vector3 respawnPoint;
    public float respawnLength;

    public GameObject deathEffect;
    public Image deathScreen;
    public Text deathText;
    public Button mainMenu;
    private bool isFadeToBlack;
    private bool isFadeFromBlack;
    public float fadeSpeed;
    public float waitForFade;

    public Slider healthBar;
    public Text healthText;
    public Text livesText;

    public GameObject pauseScreen;



    // Use this for initialization
    void Start () {
        currentHealth = maxHealth;

        healthText.text = "Health: " + currentHealth + "/" + maxHealth;
        livesText.text = "Lives: " + currentLives;
        deathText.gameObject.SetActive(false);
        mainMenu.gameObject.SetActive(false);

        respawnPoint = player.transform.position;
	}
	
	// Update is called once per frame
	void Update () {

        healthBar.maxValue = maxHealth;
        healthBar.value = currentHealth;

        if (invincibilityCounter > 0)
        {
            invincibilityCounter -= Time.deltaTime;

            flashCounter -= Time.deltaTime;

            if(flashCounter < 0)
            {
                playerRenderer.enabled = !playerRenderer.enabled;
                flashCounter = flashLength;
            }

            if(invincibilityCounter <= 0)
            {
                playerRenderer.enabled = true;
            }
        }

        if (isFadeToBlack)
        {
            deathScreen.color = new Color(deathScreen.color.r, deathScreen.color.g, deathScreen.color.b, Mathf.MoveTowards(deathScreen.color.a, 1f, fadeSpeed * Time.deltaTime));
            if(deathScreen.color.a == 1f)
            {
                isFadeToBlack = false;
            }
        }

        if (isFadeFromBlack)
        {
            deathScreen.color = new Color(deathScreen.color.r, deathScreen.color.g, deathScreen.color.b, Mathf.MoveTowards(deathScreen.color.a, 0f, fadeSpeed * Time.deltaTime));
            if (deathScreen.color.a == 0f)
            {
                isFadeFromBlack = false;
            }
        }
    }

    public void DamagePlayer(int damage, Vector3 direction)
    {
        if (invincibilityCounter <= 0)
        {
            currentHealth -= damage;
            healthText.text = "Health: " + currentHealth + "/" + maxHealth;

            if (currentHealth <= 0)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Respawn();
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                player.Knockback(direction);
                invincibilityCounter = invincibilityLength;

                playerRenderer.enabled = false;
                flashCounter = flashLength;
            }
        }
    }

    public void Respawn()
    {
        if (!isRespawning)
        {
            StartCoroutine("RespawnCo");
        }
    }

    public IEnumerator RespawnCo()
    {
        isRespawning = true;
        player.gameObject.SetActive(false);
        Instantiate(deathEffect, player.transform.position, player.transform.rotation);

        if(currentLives == 0)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                pauseScreen.gameObject.SetActive(false);
            }
            isFadeToBlack = true;
            waitForFade = 0;
            deathText.gameObject.SetActive(true);
            mainMenu.gameObject.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            yield return new WaitForSeconds(waitForFade);

        } else {

            yield return new WaitForSeconds(respawnLength);

            isFadeToBlack = true;

            yield return new WaitForSeconds(waitForFade);

            isFadeToBlack = false;
            isFadeFromBlack = true;

            isRespawning = false;

            player.gameObject.SetActive(true);
            player.transform.position = respawnPoint;
            currentHealth = maxHealth;
            currentLives--;
            invincibilityCounter = invincibilityLength;
            playerRenderer.enabled = false;
            flashCounter = flashLength;
            healthText.text = "Health: " + currentHealth + "/" + maxHealth;
            livesText.text = "Lives: " + currentLives;
        }
    }

    public void HealPlayer(int healAmount)
    {
        currentHealth += healAmount;

        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    public void SetSpawnPoint(Vector3 newPosition)
    {
        respawnPoint = newPosition;
    }
}
