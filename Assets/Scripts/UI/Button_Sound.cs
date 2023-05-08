using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Button_Sound : MonoBehaviour
{
    public void Over()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/system/menu/navigation_menu", transform.position);
    }

    public void Select()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/system/menu/menu_val", transform.position);
    }
    public void Return()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/system/menu/menu_ret", transform.position);
    }
}
