using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Etoile : MonoBehaviour
{
    private EtoilesScore ES;
    public GameObject TextTP;
    public GameObject ImageFrise;
    public float affichageFriseSpeed;
    private bool afficheFrise, once;
    public CanvasGroup frise;
    private BoxCollider bc;
    public ParticleSystem explodeParticleRenderer;
    private FMODUnity.StudioEventEmitter eventEmitter;

    private void Start()
    {
        eventEmitter = gameObject.GetComponent<FMODUnity.StudioEventEmitter>();
        ES = GetComponentInParent<EtoilesScore>();
        bc = gameObject.GetComponent<BoxCollider>();
    }
    private void Update()
    {
        if (afficheFrise && once)
        {
            bc.enabled = false;
            frise.gameObject.SetActive(true);
            frise.alpha += affichageFriseSpeed * Time.deltaTime;
            if (frise.alpha >= 1.0f)
            {
                ES.score++;
                ImageFrise.gameObject.SetActive(true);
                once = false;
            }
        }
        else if (afficheFrise)
        {
            //frise.alpha = 0.0f;
            frise.alpha -= affichageFriseSpeed * Time.deltaTime;
            if(frise.alpha <= 0.0f)
            {
                afficheFrise = false;
                frise.gameObject.SetActive(false);
                gameObject.SetActive(false);
            }
        }
        else Debug.Log("ca ce joue 3");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            frise.alpha = 0.0f;
            once = true;
            afficheFrise = true;
            eventEmitter.Stop();
            

            TextTP.SetActive(true);

            //gameObject.SetActive(false);
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(false);
            }
            FMODUnity.RuntimeManager.PlayOneShot("event:/system/Ui/etoile_collect");
            Debug.Log("parapluie prends une étoile");

            explodeParticleRenderer.Stop();
            explodeParticleRenderer.Play();


        }
    }
}
