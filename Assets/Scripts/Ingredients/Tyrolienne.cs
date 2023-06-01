using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core;
using UnityEngine;

public class Tyrolienne : MonoBehaviour
{
    public Vector3 depart, fin;
    public float timerDepart, timerArrive;
    public GameObject parapluie;
    private bool canTyrolienne;
    public bool cantMoveParapluie;
    private float timerReset;
    private player parapluiePlayer;
    public ParticleSystem tyrolienneParticleSystem;

    private void Start()
    {
        parapluiePlayer = parapluie.GetComponent<player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (/*Input.GetKeyDown(KeyCode.P) && */canTyrolienne)
        {
            tyrolienneParticleSystem.gameObject.SetActive(true);
            tyrolienneParticleSystem.Play();
            canTyrolienne = false;
            parapluiePlayer.onGround = false;
            cantMoveParapluie = true;
            parapluiePlayer.onGround = false;
            parapluiePlayer.parapluieFerme.SetActive(true);
            parapluiePlayer.parapluieOuvert.SetActive(false);
            parapluie.transform.DOMove(depart,timerDepart).OnComplete(() => DescenteTyrolienne());
            parapluie.transform.DORotateQuaternion(Quaternion.Euler(0, 0, 180), timerDepart);
            parapluiePlayer.onGround = false;
            cantMoveParapluie = true;
            parapluiePlayer.onGround = false;
            parapluiePlayer.parapluieFerme.SetActive(true);
            parapluiePlayer.parapluieOuvert.SetActive(false);
        }

        if (cantMoveParapluie)
        {
            timerReset -= Time.deltaTime;
            if (timerReset <= 0f)
            {
                cantMoveParapluie = false;
                parapluiePlayer.parapluieFerme.SetActive(false);
                parapluiePlayer.parapluieOuvert.SetActive(true);
            }
        }
    }
    public void DescenteTyrolienne()
    {
        parapluiePlayer.onGround = false;
        timerReset = timerArrive;
        parapluie.transform.DOMove(fin,timerArrive);

    }
    

    public void OnTriggerEnter(Collider other)
    {
        if( other.CompareTag("Player"))
        {
            canTyrolienne = true;
        }
    }


    private void OnTriggerExit(Collider other) {
        if( other.CompareTag("Player")){
            canTyrolienne = false;
        }
    }
}
