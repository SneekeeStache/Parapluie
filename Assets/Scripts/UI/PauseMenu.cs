using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuContainer;
    public string mainMenu;
    public GameObject parapluie;
    private player scriptParapluie;
    public GameObject canvasGroupEnd;
    public bool activeEnd;
    public GameObject end;
    public GameObject frise;
    public GameObject TitreObjectif;
    public Titre TitreObjectifTitre;
    public TextMeshProUGUI TitreObjectifText;
    public TextMeshProUGUI TitreObjectifTextMenu;
    FMOD.Studio.Bus MasterBus;

    [Header("set active false")]
    public GameObject map;
    public GameObject controle;
    private bool canvasCanEnd;
    public Etoile theShardEtoile;
    public GameObject activeRouteEndTheShard;
    public TextMeshProUGUI cheatActive;
    public bool canCheat;
    public bool isMenu;
    private bool triggerOnceUnlockEnd;

    private void Start()
    {
        MasterBus = FMODUnity.RuntimeManager.GetBus("Bus:/");
        triggerOnceUnlockEnd = true;
        scriptParapluie = parapluie.GetComponent<player>();
    }

    void Update()
    {
        if (((Input.GetKeyDown(KeyCode.M) && canCheat) || !theShardEtoile.gameObject.activeSelf) && triggerOnceUnlockEnd )
        {
            triggerOnceUnlockEnd = false;
            activeEnd = true;
            end.SetActive(true);
            activeRouteEndTheShard.SetActive(true);
            TitreObjectif.SetActive(true);
            TitreObjectifTitre.ResetTitre();
            TitreObjectifText.text = "Objectif : Retourner au point d'apparition";
            TitreObjectifTextMenu.text = "Objectif : Retourner au point d'apparition";
            
        }
        if (Input.GetButtonDown("Menu") && !canvasGroupEnd.activeSelf)
        {
            MenuActive();
        }
        //pour la fin :
        if (canvasCanEnd && parapluie.transform.position.y > 1600f)
        {
            //Debug.Log(parapluie.transform.position.y);
            canvasGroupEnd.SetActive(true);
            canvasGroupEnd.GetComponent<CanvasGroup>().alpha += (Time.deltaTime * 0.5f);
            isMenu = true;
            Cursor.lockState = CursorLockMode.None;
        }
        /*if (parapluie.transform.position.y > 1200f)
        {
            Debug.Log("manque le bool pour finir");
            Debug.Log(canvasCanEnd);
        }*/
    }

    public void Reprise()
    {
        frise.SetActive(false);
        pauseMenuContainer.SetActive(false);
        Time.timeScale = 1.0f;
        Cursor.lockState = CursorLockMode.Locked;
        map.SetActive(false);
        controle.SetActive(false);
        isMenu = false;
        //Debug.Log("reprise");
    }
    public void BackToMenu()
    {
        MasterBus.stopAllEvents(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
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
            isMenu = true;
            frise.SetActive(true);
            frise.GetComponent<CanvasGroup>().alpha = 1.0f;
            pauseMenuContainer.SetActive(true);
            Time.timeScale = 0.0f;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void MapReverse()
    {
        if (map.activeSelf) map.SetActive(false);
        else map.SetActive(true);
    }

    public void ControleReverse()
    {
        if (controle.activeSelf) controle.SetActive(false);
        else controle.SetActive(true);
    }
    public void End()
    {
        //Debug.Log("end");

        Cursor.lockState = CursorLockMode.None;
        scriptParapluie.ambiancePetit = 0;
        scriptParapluie.ambianceMoyen = 0;
        scriptParapluie.ambianceGrateCiel = 0;
        scriptParapluie.ambiancePetitCiel = 0;
        scriptParapluie.ambianceMoyenCiel = 0;
        scriptParapluie.ambianceGrateCielCiel = 0;
        scriptParapluie.ambianceWata = 0;
        scriptParapluie.ambianceSpace = 20;
        canvasCanEnd = true;

    }

    public void Cheat()
    {
        if (canCheat)
        {
            canCheat = false;
            cheatActive.text = "Cheats désactivés";
        }
        else
        {
            canCheat = true;
            cheatActive.text = "Cheats activés";
        }
        
    }
}
