using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndDetect : MonoBehaviour
{
    public EndManager EndManager;
    
    private void OnTriggerEnter(Collider other) 
    {
        if(other.CompareTag("Player"))
        {
            EndManager.End();
            //Debug.Log("Fonction End");
        }
    }
}
