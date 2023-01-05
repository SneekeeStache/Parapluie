using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrivee : MonoBehaviour
{
    private MeshRenderer mesh;
    private bool BonusPris;
    private float TimerReloadBonus;
    private float TimerReloadBonusReset = 5f;
    private Renderer triggerRenderer;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!BonusPris)
            {
                FMODUnity.RuntimeManager.PlayOneShot("event:/player/checkpoint");
                BonusPris = true;
            }
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
