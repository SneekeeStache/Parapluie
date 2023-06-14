using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class PauseMenu : MonoBehaviour
{
    [Header("Composants à récupérer")]
    
    public GameObject PauseMenuContainer;
    public GameObject MapInMenu;
    public GameObject ControleInMenu;
    public GameObject frise;
    public Player Parapluie;
    
    [Header("propriétés")]
    
    public bool isMenu;
    public bool CanPause;
    void Update()
    {
        if (Input.GetButtonDown("Menu") && CanPause)
        {
            MenuActive();
        }
    }
    public void Reprise()
    {
        frise.SetActive(false);
        PauseMenuContainer.SetActive(false);
        Time.timeScale = 1.0f;
        Cursor.lockState = CursorLockMode.Locked;
        MapInMenu.SetActive(false);
        ControleInMenu.SetActive(false);
        isMenu = false;
        //Debug.Log("reprise");
        Parapluie.DisableMove = false;
    }
    public void MenuActive()
    {
        if (PauseMenuContainer.activeSelf)
        {
            Reprise();
        }
        else
        {
            Parapluie.DisableMove = true;
            isMenu = true;
            frise.SetActive(true);
            frise.GetComponent<CanvasGroup>().alpha = 1.0f;
            PauseMenuContainer.SetActive(true);
            Debug.Log(PauseMenuContainer.activeSelf);
            Time.timeScale = 0.0f;
            Cursor.lockState = CursorLockMode.None;
        }
    }
    public void MapReverse()
    {
        if (MapInMenu.activeSelf) MapInMenu.SetActive(false);
        else MapInMenu.SetActive(true);
    }
    public void ControleInMenuReverse()
    {
        if (ControleInMenu.activeSelf) ControleInMenu.SetActive(false);
        else ControleInMenu.SetActive(true);
    }
}
