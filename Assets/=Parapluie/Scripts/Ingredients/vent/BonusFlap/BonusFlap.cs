using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class BonusFlap : MonoBehaviour
{
    
    [Header("Composants à récupérer")]
    public GameObject ParticleSystemWind;
    public Transform Parapluie;
    public ParapluieFeedBack ParapluieFeedBack;
    
    [Header("fonctionnement du bonus")]
    private bool collected;
    private float timerReloadBonus;
    public float TimerReloadBonusReset = 5f;
    
    [Header("desactiver le renderer avec la distance")]
    public float distance;
    public float distancePourDisparaitre = 2000f;

    [SerializeField] private GameObject trails;

    // Private variables
    
    private Player player;
    private MeshRenderer meshRenderer;
    
    public Player Player => player;
    
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("collider sans tag" + other.name);
        if (other.CompareTag("Player"))
        {
            //Debug.Log("collider avec tag" + other.name);
            if (!collected)
            {
                ExplodeParticleBonus();
                FMODUnity.RuntimeManager.PlayOneShot("event:/Parapluie/bonus");
                collected = true;
                
                trails.SetActive(true);
            }
        }
    }
    private void Start()
    {
        Parapluie = GameObject.FindWithTag("Player").transform;
        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.enabled = false;
        timerReloadBonus = TimerReloadBonusReset;
        player = Parapluie.GetComponent<Player>();
    }
    private void Update()
    {
        distance = Vector3.Distance(Parapluie.transform.position, gameObject.transform.position);
        //distance = MathF.Abs(distance);
        if (collected)
        {
            ParticleSystemWind.SetActive(false);
            timerReloadBonus -= Time.deltaTime;
        }
        
        //faire disparaitre le wind renderer quand il est trop loin
        if (timerReloadBonus <= 0f && distance <= distancePourDisparaitre)
        {
            timerReloadBonus = TimerReloadBonusReset;
            ParticleSystemWind.SetActive(true);
            collected = false;
        }
        else if (!collected &&distance <= distancePourDisparaitre)
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
        ParapluieFeedBack.BonusFlapFeedBack();
    }
}
