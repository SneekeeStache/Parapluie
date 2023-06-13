using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class BonusFlap : MonoBehaviour
{
    
    [Header("Composants à récupérer")]
    private Player player;
    private MeshRenderer MR;
    public GameObject ParticleSystemWind;
    public ParticleSystem explodeParticleRenderer;
    public Transform Parapluie;
    public ParapluieFeedBack parapluieFeedBack;
    
    [Header("fonctionnement du bonus")]
    private bool BonusPris;
    private float TimerReloadBonus;
    public float TimerReloadBonusReset = 5f;
    
    [Header("desactiver le renderer avec la distance")]
    public float distance;
    public float distancePourDisparaitre = 2000f;

    
    
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("collider sans tag" + other.name);
        if (other.CompareTag("Player"))
        {
            //Debug.Log("collider avec tag" + other.name);
            if (!BonusPris)
            {
                ExplodeParticleBonus();
                player.EnergieFlap = 100;
                FMODUnity.RuntimeManager.PlayOneShot("event:/player/bonus");
                BonusPris = true;
            }
        }
    }
    private void Start()
    {
        Parapluie = GameObject.FindWithTag("Player").transform;
        MR = GetComponent<MeshRenderer>();
        MR.enabled = false;
        TimerReloadBonus = TimerReloadBonusReset;
        player = Parapluie.GetComponent<Player>();
    }
    private void Update()
    {
        distance = Vector3.Distance(Parapluie.transform.position, gameObject.transform.position);
        //distance = MathF.Abs(distance);
        if (BonusPris)
        {
            ParticleSystemWind.SetActive(false);
            TimerReloadBonus -= Time.deltaTime;
        }
        
        //faire disparaitre le wind renderer quand il est trop loin
        if (TimerReloadBonus <= 0f && distance <= distancePourDisparaitre)
        {
            TimerReloadBonus = TimerReloadBonusReset;
            ParticleSystemWind.SetActive(true);
            BonusPris = false;
        }
        else if (!BonusPris &&distance <= distancePourDisparaitre)
        {
            ParticleSystemWind.SetActive(true);
        }
        else if (distance >= distancePourDisparaitre)
        {
            ParticleSystemWind.SetActive(false);
        }
    }
    public void ExplodeParticleBonus()
    {
        explodeParticleRenderer.Stop();
        explodeParticleRenderer.Play();
        parapluieFeedBack.BonusFlapFeedBack();
    }
}
