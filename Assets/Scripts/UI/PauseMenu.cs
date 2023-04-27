using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuContainer;
    public string mainMenu;
    private GameObject parapluie;
    public GameObject canvasGroupEnd;
    public bool activeEnd;

    [Header("set active false")]
    public GameObject map;
    public GameObject controle;

    private void Start()
    {
        parapluie = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        if (Input.GetButtonDown("Menu"))
        {
            MenuActive();
        }
        //pour la fin :
        if (parapluie.transform.localPosition.y >= 1100  && activeEnd)
        {
            canvasGroupEnd.SetActive(true);
            canvasGroupEnd.GetComponent<CanvasGroup>().alpha += (Time.deltaTime * 0.5f);
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void Reprise()
    {
        pauseMenuContainer.SetActive(false);
        Time.timeScale = 1.0f;
        Cursor.lockState = CursorLockMode.Locked;
        map.SetActive(false);
        controle.SetActive(false);
    }
    public void BackToMenu()
    {
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene(mainMenu);
    }




    public void MenuActive()
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
