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
        playerScript.ambiancePetit = 0;
        playerScript.ambianceMoyen = 0;
        playerScript.ambianceGrateCiel = 20;
        playerScript.ambiancePetitCiel = 0;
        playerScript.ambianceMoyenCiel = 0;
        playerScript.ambianceGrateCielCiel = 0;
    }
}
