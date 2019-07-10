using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    // Use this for initialization
	void Start () {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = (true);
    }
	
    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
