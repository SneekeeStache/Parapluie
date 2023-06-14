using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrivee : MonoBehaviour
{
    public GameObject Validation;
    private MeshRenderer mesh;
    private bool BonusPris;
    private float TimerReloadBonus;
    private float TimerReloadBonusReset = 5f;
    private Renderer triggerRenderer;

    private void Start()
    {
        mesh = gameObject.GetComponent<MeshRenderer>();
        Validation.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!BonusPris)
            {
                FMODUnity.RuntimeManager.PlayOneShot("event:/Parapluie/checkpoint");
                BonusPris = true;
                other.transform.position = new Vector3(0f,20f,0f);
                Validation.SetActive(true);
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
