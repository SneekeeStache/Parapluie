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

        if (Input.GetKeyDown(KeyCode.N))
        {
            Cheat();
        }
    }

    public void Cheat()
    {
        if (canCheat)
        {
            canCheat = false;
            cheatActive.text = "Cheats désactivés";
            Parapluie.CHEAT = false;
        }
        else
        {
            canCheat = true;
            cheatActive.text = "Cheats activés";
            Parapluie.CHEAT = true;
        }
        
    }
}
