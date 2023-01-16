using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetFlap : MonoBehaviour
{
    public player Parapluie;
    private bool triggerOnceFmod = true;

    private void OnTriggerStay(Collider other)
    {
        //si le ActiveTimer n'est pas là, il joue la condition la frame après le saut donc il reset le nombre de saut juste après le premier saut
        if (other.CompareTag("Ground") && Parapluie.ActiveTimer == false)
        {
            //if (triggerOnceFmod) FMODUnity.RuntimeManager.PlayOneShot("event:/player/regenate_flap");
            if (Parapluie.NombreFlap >= Parapluie.FlapingNumber) Parapluie.FlapingNumber = Parapluie.NombreFlap;
            triggerOnceFmod = false;

            if (!Parapluie.onGround)
            {
                triggerOnceFmod = true;
                Parapluie.groundPosition = new Vector3(transform.position.x, Parapluie.transform.position.y, transform.position.z);
            }
            Parapluie.onGround = true;
            //FMODUnity.RuntimeManager.PlayOneShot("event:/player/landing");
        }
    }

}
