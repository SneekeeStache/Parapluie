using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusFlap : MonoBehaviour
{
    [Range(1,5)]
    public int NombreDeFlapEnBonus;

    [SerializeField] private Color color1;
    [SerializeField] private Color color2;
    [SerializeField] private Color color3;
    [SerializeField] private Color color4;
    [SerializeField] private Color color5;

    private Renderer triggerRenderer;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<player>().FlapingNumber += NombreDeFlapEnBonus;
        }
    }

    private void Start()
    {
        triggerRenderer = gameObject.GetComponent<Renderer>();
        
        switch (NombreDeFlapEnBonus)
        {
            case 1:
                triggerRenderer.material.SetColor("_Color",color1);
                break;
            case 2:
                triggerRenderer.material.SetColor("_Color",color2);
                break;
            case 3:
                triggerRenderer.material.SetColor("_Color",color3);
                break;
            case 4:
                triggerRenderer.material.SetColor("_Color",color4);
                break;
            case 5:
                triggerRenderer.material.SetColor("_Color",color5);
                break;
        }
    }
}
