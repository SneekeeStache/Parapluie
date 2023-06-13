using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerfectIndicator : MonoBehaviour
{
    public Player Player;

    public Material CrayonParapluie;
    public Material PerfectIndicatorMaterial;
    
    public float perfectIndicatorScale;
    public float perfectIndicatorScaleReset;
    public float perfectIndicatorScaleSpeedDown;
    
    private float perfectFeedbackTimer;
    
    public bool perfectIndicatorBool;
    
    
    void Update()
    {
        perfectIndicatorScale -= perfectIndicatorScaleSpeedDown * Time.deltaTime;
        gameObject.transform.localScale = new Vector3(perfectIndicatorScale, perfectIndicatorScale, perfectIndicatorScale);
        
        if(perfectIndicatorScale <= 0f)
        {
            perfectIndicatorScale = 0f; ;
        }
        
        if (!Player.EnergieDown && (Player.EnergieFlap == Player.EnergieRW || (Player.EnergieFlap < Player.EnergieRW && Player.EnergieRW - Player.EnergieFlap <= 3) || (Player.EnergieFlap > Player.EnergieRW && Player.EnergieFlap - Player.EnergieRW <= 3)))
        {
            PerfectIndicatorMaterial.SetColor("_EmissionColor", CrayonParapluie.GetColor("_BaseColor"));
        }
        else
        {
            perfectIndicatorBool = false;
        }
    }
    
    public void PerfectIndicatorReset()
    {
        perfectIndicatorBool = true;
        PerfectIndicatorMaterial.SetColor("_EmissionColor", Color.white);
        perfectIndicatorScale = perfectIndicatorScaleReset;
        gameObject.transform.localScale = new Vector3(perfectIndicatorScale, perfectIndicatorScale, perfectIndicatorScale);
    }
}
