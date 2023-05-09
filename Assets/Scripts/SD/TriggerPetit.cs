using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPetit : MonoBehaviour
{
    // Start is called before the first frame update
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
        playerScript.ambiancePetit = 20;
        playerScript.ambianceMoyen = 0;
        playerScript.ambianceGrateCiel = 0;
        playerScript.ambiancePetitCiel = 0;
        playerScript.ambianceMoyenCiel = 0;
        playerScript.ambianceGrateCielCiel = 0;
    }
}
