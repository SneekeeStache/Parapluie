using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetFlap : MonoBehaviour
{
    public Player Parapluie;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Ground") && Parapluie.ActiveTimer == false)
        {
            Parapluie.EnergieFlap = 100f;

            if (!Parapluie.onGround)
            {
                Parapluie.groundPosition = new Vector3(transform.position.x, Parapluie.transform.position.y, transform.position.z);
            }
            Parapluie.onGround = true;
        }
    }
}
