using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Button_Sound : MonoBehaviour
{
    public void Over()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/UI/Sfx_Bouton_Clicker", transform.position);
    }

    public void Select()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/UI/Sfx_Boite_Dialogue_Ouvrir", transform.position);
    }
    public void Return()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/UI/Sfx_Boite_Dialogue_Fermer", transform.position);
    }
}
