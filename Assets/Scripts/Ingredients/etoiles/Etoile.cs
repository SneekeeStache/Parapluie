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

    private void Start()
    {
        ES = GetComponentInParent<EtoilesScore>();
        bc = gameObject.GetComponent<BoxCollider>();
    }
    private void Update()
    {
        if (afficheFrise && once)
        {
            bc.enabled = false;
            Debug.Log("ca ce joue");
            frise.gameObject.SetActive(true);
            frise.alpha += affichageFriseSpeed * Time.deltaTime;
            if (frise.alpha >= 1.0f)
            {
                ImageFrise.gameObject.SetActive(true);
                once = false;
            }
        }
        else if (afficheFrise)
        {
            Debug.Log("ca ce joue 2");
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
        Debug.Log(afficheFrise);
        Debug.Log(once);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            frise.alpha = 0.0f;
            once = true;
            afficheFrise = true;
            
            
            ES.score++;
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
