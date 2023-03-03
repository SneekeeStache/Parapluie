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
            }
            else
            {
                pauseMenuContainer.SetActive(true);
                Time.timeScale = 0.0f;
            }
        }
    }

    public void Reprise()
    {
        pauseMenuContainer.SetActive(true);
        Time.timeScale = 1.0f;
    }
    public void BackToMenu()
    {
        SceneManager.LoadScene(mainMenu);
    }
}
