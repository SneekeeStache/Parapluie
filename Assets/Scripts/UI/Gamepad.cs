using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class Gamepad : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject newGame;
    [SerializeField] private GameObject Level1;
    [SerializeField] private GameObject Controls;
    [SerializeField] private GameObject Credits;
    private GameObject gameObjectActuel;
    private Vector3 mousePosition;
    private bool selectMenuController;
    
    
    void Start()
    {
        EventSystem.current.SetSelectedGameObject(newGame);
        gameObjectActuel = newGame;
    }

    private void Awake()
    {
        EventSystem.current.SetSelectedGameObject(newGame);
        gameObjectActuel = newGame;
    }

    void Update()
    {
        //selection à la manette
        if ((Input.GetAxis("Vertical") > 0 || Input.GetAxis("Vertical") < 0 ) && selectMenuController == false)
        {
            
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(gameObjectActuel);
            selectMenuController = true;
        }
        // selection au clavier
        if (Input.mousePosition != mousePosition)
        {
            selectMenuController = false;
            EventSystem.current.SetSelectedGameObject(null);
        }
        mousePosition = Input.mousePosition;
        
        //retour dans les menus
        /*if (Input.GetButtonDown("Back"))
        {
            gameObjectActuel = newGame;
            EventSystem.current.SetSelectedGameObject(gameObjectActuel);
        }*/
    }

    public void choixNiveau()
    {
        EventSystem.current.SetSelectedGameObject(Level1);
        gameObjectActuel = Level1;
    }

    public void ChoixControls()
    {
        EventSystem.current.SetSelectedGameObject(Controls);
        gameObjectActuel = Controls;
    }

    public void choixCredits()
    {
        EventSystem.current.SetSelectedGameObject(Credits);
        gameObjectActuel = Credits;
    }

    public void retour()
    {
        EventSystem.current.SetSelectedGameObject(newGame);
        gameObjectActuel = newGame;
    }
    
    
}
