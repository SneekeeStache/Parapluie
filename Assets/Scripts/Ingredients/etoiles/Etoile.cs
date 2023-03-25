using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Etoile : MonoBehaviour
{
    private EtoilesScore ES;
    public GameObject TextTP;

    private void Start()
    {
        ES = GetComponentInParent<EtoilesScore>();
    }

    private void OnTriggerEnter(Collider other)
    {
        ES.score++;
        TextTP.SetActive(true);
        gameObject.SetActive(false);
    }
}
