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

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && canTyrolienne)
        {
            cantMoveParapluie = true;
            parapluie.transform.DOMove(depart,timerDepart).OnComplete(() => DescenteTyrolienne());
            parapluie.transform.DORotateQuaternion(Quaternion.Euler(0, 0, 180), timerDepart);
        }

        if (cantMoveParapluie)
        {
            timerReset -= Time.deltaTime;
            if (timerReset <= 0f)
            {
                cantMoveParapluie = false;
            }
        }
    }
    public void DescenteTyrolienne()
    {
        parapluie.GetComponent<player>().onGround = false;
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
