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
    void Start()
    {
        EventSystem.current.SetSelectedGameObject(newGame);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void choixNiveau()
    {
        EventSystem.current.SetSelectedGameObject(Level1);
    }

    public void ChoixControls()
    {
        EventSystem.current.SetSelectedGameObject(Controls);
    }

    public void choixCredits()
    {
        EventSystem.current.SetSelectedGameObject(Credits);
    }

    public void retour()
    {
        EventSystem.current.SetSelectedGameObject(newGame);
    }
    
    
}
