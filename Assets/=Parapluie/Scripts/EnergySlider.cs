using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergySlider : MonoBehaviour
{
    public Player Player;
    public Image SelfImage;
    public Image EnergyRealImage;
    private float FillAmount;
    public float lerpTimer;
    public float chipSpeed;

    
    [Header("Alpha of Energy bar")]
    public CanvasGroup CG;

    private float timer;
    public float timerReset;
    public float SpeedAppear;
    public float SpeedDisappear;
    void Start()
    {
        FillAmount = SelfImage.fillAmount;
        CG.alpha = 1f;
    }
    
    void Update()
    {
        EnergyRealImage.fillAmount = Player.EnergieFlap/100 * FillAmount;
        float EnegergyFraction = Player.EnergieFlap / 100 * FillAmount;
        
        // si le fillamount est plus grands que l'energie, alors on a flap et alors on joue l'animation de l'energie qui descent.
        
        if (Player.EnergieFlap == 100f)
        {
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete = percentComplete * percentComplete;
            SelfImage.fillAmount = Mathf.Lerp(SelfImage.fillAmount, EnegergyFraction, percentComplete);
            if (SelfImage.fillAmount == FillAmount) lerpTimer = 0f;
        }
        else if (SelfImage.fillAmount > Player.EnergieFlap / 100 * FillAmount)
        {
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete = percentComplete * percentComplete;
            SelfImage.fillAmount = Mathf.Lerp(SelfImage.fillAmount, EnegergyFraction, percentComplete);
        }
        else
        {
            lerpTimer = 0f;
            SelfImage.fillAmount = Player.EnergieFlap/100 * FillAmount;
        }

        if (Player.EnergieFlap == 100f)
        {
            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                /* j'y arrive pas ca m'enerve
                lerpTimer += Time.deltaTime;
                float percentComplete = lerpTimer / (chipSpeed * SpeedDisappear) ;
                percentComplete = percentComplete * percentComplete;
                CG.alpha = Mathf.Lerp(1f, 0f, percentComplete);*/
                CG.alpha -= Time.deltaTime * SpeedDisappear;
            }
        }
        else
        {
            timer = timerReset;
            CG.alpha += Time.deltaTime * SpeedAppear;
            
        }

        //if (Input.GetKeyDown(KeyCode.F)) CG.alpha = 1f;


    }
}
