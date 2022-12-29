using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusFlap : MonoBehaviour
{
    public float NombreDeFlapEnBonus;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<player>().FlapingNumber += NombreDeFlapEnBonus;
        }
    }
}
