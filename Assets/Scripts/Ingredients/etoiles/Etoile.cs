using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Etoile : MonoBehaviour
{
    private EtoilesScore ES;

    private void Start()
    {
        ES = GetComponentInParent<EtoilesScore>();
    }

    private void OnTriggerEnter(Collider other)
    {
        ES.score++;
        gameObject.SetActive(false);
    }
}
