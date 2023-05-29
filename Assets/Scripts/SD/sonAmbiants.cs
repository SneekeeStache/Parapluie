using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sonAmbiants : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] FMODUnity.StudioEventEmitter SonAmbiance;
    [SerializeField] player playerScript;
    [SerializeField] Transform playerLocation;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerScript.ambianceWata > 0)
        {
            SonAmbiance.SetParameter("distance sol", 0);
        }
        else
        {
            SonAmbiance.SetParameter("distance sol", playerLocation.position.y);
        }
        
    }
}
