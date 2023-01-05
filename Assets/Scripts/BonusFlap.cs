using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusFlap : MonoBehaviour
{
    [Range(1, 5)]
    public int NombreDeFlapEnBonus;
    private MeshRenderer mesh;
    private bool BonusPris;
    private float TimerReloadBonus;
    private float TimerReloadBonusReset = 5f;

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
            if (!BonusPris)
            {
                other.GetComponent<player>().FlapingNumber += NombreDeFlapEnBonus;
                FMODUnity.RuntimeManager.PlayOneShot("event:/player/bonus");
                BonusPris = true;
            }
        }
    }

    private void Start()
    {
        mesh = gameObject.GetComponent<MeshRenderer>();
        TimerReloadBonus = TimerReloadBonusReset;
        triggerRenderer = gameObject.GetComponent<Renderer>();

        switch (NombreDeFlapEnBonus)
        {
            case 1:
                triggerRenderer.material.SetColor("_Color", color1);
                break;
            case 2:
                triggerRenderer.material.SetColor("_Color", color2);
                break;
            case 3:
                triggerRenderer.material.SetColor("_Color", color3);
                break;
            case 4:
                triggerRenderer.material.SetColor("_Color", color4);
                break;
            case 5:
                triggerRenderer.material.SetColor("_Color", color5);
                break;
        }
    }
    private void Update()
    {
        if (BonusPris)
        {
            mesh.enabled = false;
            TimerReloadBonus -= Time.deltaTime;
        }
        if (TimerReloadBonus <= 0f)
        {
            TimerReloadBonus = TimerReloadBonusReset;
            mesh.enabled = true;
            BonusPris = false;
        }
    }
}
