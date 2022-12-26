using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetFlap : MonoBehaviour
{
    public player Parapluie;
    void Start()
    {

    }


    void Update()
    {

    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Ground") && Parapluie.ActiveTimer == false)
        {
            Parapluie.FlapingNumber = Parapluie.NombreFlap;
        }
    }
    
}
