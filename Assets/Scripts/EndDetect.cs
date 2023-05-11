using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndDetect : MonoBehaviour
{
    public PauseMenu pm;


    private void OnTriggerEnter(Collider other) 
    {
        if(other.CompareTag("Player"))
        {
            pm.End();
        }
    }
}
