using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    public string mainMenuScene;
    public GameObject pauseMenu;
    public bool isPaused;
    public CameraControl cam;

    // Use this for initialization
    void Start () {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                ResumeGame();
            } else
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                cam.enabled = false;
                isPaused = true;
                pauseMenu.SetActive(true);
                Time.timeScale = 0f;
            }
        }

    }

    public void ResumeGame()
    {
        isPaused = false;
        pauseMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1f;
        cam.enabled = true;
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f;
        cam.enabled = true;
        SceneManager.LoadScene(mainMenuScene);
    }
}
