using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPetitCiel : MonoBehaviour
{
    [SerializeField] Player playerScript;
    // Start is called before the first frame update
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
            playerScript.ambianceGrateCiel = 0;
            playerScript.ambiancePetitCiel = 20;
            playerScript.ambianceMoyenCiel = 0;
            playerScript.ambianceGrateCielCiel = 0;
            playerScript.ambianceWata = 0;
            playerScript.ambianceSpace = 0;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerScript.ambiancePetitCiel = 0;
        }
    }
}
