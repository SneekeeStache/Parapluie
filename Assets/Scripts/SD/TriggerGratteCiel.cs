using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerGratteCiel : MonoBehaviour
{
    [SerializeField] player playerScript;

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
            playerScript.ambianceMoyen = 0;
            playerScript.ambianceGrateCiel = 20;
            playerScript.ambiancePetitCiel = 0;
            playerScript.ambianceMoyenCiel = 0;
            playerScript.ambianceGrateCielCiel = 0;
            playerScript.ambianceWata = 0;
            playerScript.ambianceSpace = 0;
        }
        
    }
}
