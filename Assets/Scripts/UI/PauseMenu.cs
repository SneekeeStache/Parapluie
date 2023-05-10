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
    private player scriptParapluie;
    public GameObject canvasGroupEnd;
    public bool activeEnd;

    [Header("set active false")]
    public GameObject map;
    public GameObject controle;

    private void Start()
    {
        parapluie = GameObject.FindWithTag("Player");
        scriptParapluie=parapluie.GetComponent<player>();
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
            scriptParapluie.ambiancePetit = 0;
            scriptParapluie.ambianceMoyen = 0;
            scriptParapluie.ambianceGrateCiel = 0;
            scriptParapluie.ambiancePetitCiel = 0;
            scriptParapluie.ambianceMoyenCiel = 0;
            scriptParapluie.ambianceGrateCielCiel = 0;
            scriptParapluie.ambianceWata = 0;
            scriptParapluie.ambianceSpace = 20;

        }
    }

    public void Reprise()
    {
        pauseMenuContainer.SetActive(false);
        Time.timeScale = 1.0f;
        Cursor.lockState = CursorLockMode.Locked;
        map.SetActive(false);
        controle.SetActive(false);
        Debug.Log("reprise");
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
            Reprise();                
        }
        else
        {
            pauseMenuContainer.SetActive(true);
            Time.timeScale = 0.0f;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void MapReverse()
    {
        if(map.activeSelf) map.SetActive(false);
        else map.SetActive(true);
    }

    public void ControleReverse()
    {
        if (controle.activeSelf) controle.SetActive(false);
        else controle.SetActive(true);
    }
}
