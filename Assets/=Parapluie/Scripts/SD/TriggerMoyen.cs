using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerMoyen : MonoBehaviour
{
    [SerializeField] Player playerScript;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerScript.ambiancePetit = 0;
            playerScript.ambianceMoyen = 20;
            playerScript.ambianceGrateCiel = 0;
            playerScript.ambiancePetitCiel = 0;
            playerScript.ambianceMoyenCiel = 0;
            playerScript.ambianceGrateCielCiel = 0;
            playerScript.ambianceWata = 0;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerScript.ambianceMoyen = 0;
        }
    }
}
