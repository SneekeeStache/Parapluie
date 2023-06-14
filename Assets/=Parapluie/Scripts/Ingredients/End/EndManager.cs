using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class EndManager : MonoBehaviour
{
    [Header("Composants à récupérer")]
    
    public CanvasGroup CanvasEndWorldSpace;
    public Player Parapluie;
    public Etoile EtoileForEnd;
    public CheatManager CheatManager;
    public GameObject RouteForEnd;
    public PauseMenu PauseMenu;
    
    public Titre TitreObjectifTitre;
    public TextMeshProUGUI TitreObjectifText;
    public TextMeshProUGUI TitreObjectifTextMenu;
    public GameObject TitreObjectif;

    
    [Header("Propriétés")]
    public float HightForEnd;
    public string TextForEnd;

    [Header("Fonctionement")]
    
    private bool AscensionEnd; //s'envole dans le vent de fin
    public GameObject EndContainer;
    
    void Update()
    {
        //debloque la route de la fin et la fin
        if ((Input.GetKeyDown(KeyCode.M) && CheatManager.canCheat) || EtoileForEnd.once)
        {
            EndContainer.SetActive(true);
            RouteForEnd.SetActive(true);
            TitreObjectif.SetActive(true);
            TitreObjectifTitre.ResetTitre();
            TitreObjectifText.text = TextForEnd;
            TitreObjectifTextMenu.text = TextForEnd;
        }
        
        //la fin de la fin du jeu
        if (AscensionEnd && Parapluie.transform.position.y > HightForEnd)
        {
            //Debug.Log(Parapluie.transform.position.y);
            CanvasEndWorldSpace.gameObject.SetActive(true);
            CanvasEndWorldSpace.GetComponent<CanvasGroup>().alpha += (Time.deltaTime * 0.5f);
            PauseMenu.isMenu = true;
            Cursor.lockState = CursorLockMode.None;
            PauseMenu.CanPause = false;
        }

        if (AscensionEnd)
        {
            CanvasEndWorldSpace.alpha += (Time.deltaTime * 0.1f);
        }
    }
    
    public void End()
    {
        //Debug.Log("EndContainer");
        Parapluie.ambiancePetit = 0;
        Parapluie.ambianceMoyen = 0;
        Parapluie.ambianceGrateCiel = 0;
        Parapluie.ambiancePetitCiel = 0;
        Parapluie.ambianceMoyenCiel = 0;
        Parapluie.ambianceGrateCielCiel = 0;
        Parapluie.ambianceWata = 0;
        Parapluie.ambianceSpace = 20;
        AscensionEnd = true;
    }
}
