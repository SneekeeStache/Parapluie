using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vent : MonoBehaviour
{
    [SerializeField] Vector3 ajoutVent;
    [SerializeField] player PlayerScript;
    [SerializeField] float timerImpulseVent;
    [SerializeField] float timerResetValue = 0.5f;
    [SerializeField] bool ventContinue = false;

    float timer;
    float timerReset = 0;


    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (ventContinue)
            {
                PlayerScript.OrientationVent = PlayerScript.DefaultOrientationVent;
            }
            else
            {
                if (timer < timerImpulseVent)
                {
                    PlayerScript.OrientationVent = PlayerScript.DefaultOrientationVent;
                    timer += Time.deltaTime;
                }
                else
                {
                    PlayerScript.OrientationVent = ajoutVent;
                    if (timerReset < timerResetValue)
                    {
                        timerReset += Time.deltaTime;
                    }
                    else
                    {
                        timerReset = 0;
                        timer = 0;
                    }

                }
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        PlayerScript.OrientationVent = PlayerScript.DefaultOrientationVent;
    }
}
