using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CheatManager : MonoBehaviour
{
    public Player Parapluie;
    public TextMeshProUGUI cheatActive;
    public bool canCheat;

    private void Update()
    {
        if (Input.GetButtonDown("Gainenergie") && canCheat)
        {
            Parapluie.EnergieFlap = 100f;
        }
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
