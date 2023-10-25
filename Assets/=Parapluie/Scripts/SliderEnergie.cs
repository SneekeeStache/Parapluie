using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SliderEnergie : MonoBehaviour
{
    public Player Parapluie;
    
    public Slider sliderEnergie;
    public Slider SliderEnergieRW;
    public Image SliderBG;

    
    public Color ColorSliderUp;
    public Color ColorSliderDown;
    void Start()
    {
        
    }


    void Update()
    {

        if (Parapluie.EnergieFlap >= 100)
        {
            SliderBG.color = ColorSliderUp;
        }

        if (Parapluie.EnergieDown)
        {
            SliderBG.color = ColorSliderDown;
        }
        
        sliderEnergie.value = Parapluie.EnergieFlap;
        SliderEnergieRW.value = Parapluie.EnergieRW;
    }
}
