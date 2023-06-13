using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConvertTriggerToButton : MonoBehaviour
{
    public bool triggerR;
    public bool triggerL;
    private float triggerValeurR;
    private float triggerValeurL;


    void LateUpdate()
    {
        if (triggerValeurR < 0.5f && Input.GetAxis("FlapTrigger") >= 0.5f)
        {
            triggerR = true;
        }
        else
        {
            triggerR = false;
        }
        if (triggerValeurL < 0.5f && Input.GetAxis("MegaflapTrigger") >= 0.5f)
        {
            triggerL = true;
        }
        else
        {
            triggerL = false;
        }
        triggerValeurR = Input.GetAxis("FlapTrigger");
        triggerValeurL = Input.GetAxis("MegaflapTrigger");
        
    }
}
