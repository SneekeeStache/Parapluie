using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EtoileBaisseSon : MonoBehaviour
{
    [SerializeField] Player scriptPlayer;
    // Start is called before the first frame update

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (scriptPlayer.ambiancePetit > 0)
            {
                scriptPlayer.ambiancePetit = 10;
            }
            else if (scriptPlayer.ambianceMoyen > 0)
            {
                scriptPlayer.ambianceMoyen = 10;
            }
            else if (scriptPlayer.ambianceGrateCiel > 0)
            {
                scriptPlayer.ambianceGrateCiel = 10;
            }
            else if (scriptPlayer.ambiancePetitCiel > 0)
            {
                scriptPlayer.ambiancePetitCiel = 10;
            }
            else if (scriptPlayer.ambianceMoyenCiel > 0)
            {
                scriptPlayer.ambianceMoyenCiel = 10;
            }
            else if (scriptPlayer.ambianceGrateCielCiel > 0)
            {
                scriptPlayer.ambianceGrateCielCiel = 10;
            }
            else if (scriptPlayer.ambianceWata > 0)
            {
                scriptPlayer.ambianceWata = 10;
            }
        }
        
    }
}
