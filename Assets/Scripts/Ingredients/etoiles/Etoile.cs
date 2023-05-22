using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Etoile : MonoBehaviour
{
    private EtoilesScore ES;
    public GameObject TextTP;
    public GameObject ImageFrise;


    private void Start()
    {
        ES = GetComponentInParent<EtoilesScore>();

    }

    private void OnTriggerEnter(Collider other)
    {
        ImageFrise.gameObject.SetActive(true);
        ES.score++;
        TextTP.SetActive(true);
        gameObject.SetActive(false);
        FMODUnity.RuntimeManager.PlayOneShot("event:/system/Ui/etoile_collect");
    }


}
