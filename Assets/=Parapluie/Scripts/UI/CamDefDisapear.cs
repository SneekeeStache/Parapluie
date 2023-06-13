using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CamDefDisapear : MonoBehaviour
{
    private CanvasGroup cg;
    public float speedDisappear;
    
    void Start()
    {
        cg = gameObject.GetComponent<CanvasGroup>();
    }


    void Update()
    {
        cg.alpha -= speedDisappear * Time.deltaTime;
    }
}
