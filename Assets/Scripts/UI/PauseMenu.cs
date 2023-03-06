using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuContainer;
    public string mainMenu;

    void Update()
    {
        if (Input.GetButtonDown("Menu"))
        {
            if (pauseMenuContainer.activeSelf)
            {
                pauseMenuContainer.SetActive(false);
                Time.timeScale = 1.0f;
                Cursor.lockState = CursorLockMode.Locked;
                
            }
            else
            {
                pauseMenuContainer.SetActive(true);
                Time.timeScale = 0.0f;
                Cursor.lockState = CursorLockMode.None;
            }
        }
    }

    public void Reprise()
    {
        pauseMenuContainer.SetActive(false);
        Time.timeScale = 1.0f;
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void BackToMenu()
    {
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene(mainMenu);
    }
}
