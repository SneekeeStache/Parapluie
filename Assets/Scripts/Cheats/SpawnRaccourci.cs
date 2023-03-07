using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SpawnRaccourci : MonoBehaviour
{
    public GameObject Parapluie;
    private List<Transform> Ateliers;
    public int AtelierTeleport;

    private void Start()
    {
        Ateliers = GetComponentsInChildren<Transform>().ToList();
        Ateliers.RemoveAt(0);
    }

    void Update()
    {
        //if( Input.GetKeyDown(KeyCode.F5)) SceneManager.LoadScene("test");
        //téléportations aux points de la liste
        if(Input.GetButtonDown("CheatSpawn"))
        {
            Parapluie.GetComponent<player>().flap();
            Parapluie.GetComponent<CapsuleCollider>().enabled = false;
            Parapluie.GetComponent<player>().colliderParapluie.SetActive(false);
            Parapluie.GetComponent<player>().Collision = false;
            Parapluie.transform.position = Ateliers[AtelierTeleport].transform.position;
            AtelierTeleport += 1;
            Parapluie.GetComponent<player>().FlapingNumber = Parapluie.GetComponent<player>().NombreFlap;
            if(AtelierTeleport > Ateliers.Count-1) AtelierTeleport = 0;
            Parapluie.GetComponent<player>().Collision = true;
            Parapluie.GetComponent<player>().colliderParapluie.SetActive(true);
            Parapluie.GetComponent<CapsuleCollider>().enabled = true;
        }
        if(Input.GetKeyDown(KeyCode.U))
        {
            Parapluie.GetComponent<player>().flap();
            Parapluie.GetComponent<player>().Collision = false;
            Parapluie.GetComponent<player>().colliderParapluie.SetActive(false);
            Parapluie.GetComponent<CapsuleCollider>().enabled = false;
            Parapluie.transform.position = Ateliers[AtelierTeleport].transform.position;
            AtelierTeleport -= 1;
            Parapluie.GetComponent<player>().FlapingNumber = Parapluie.GetComponent<player>().NombreFlap;
            if(AtelierTeleport <= -1) AtelierTeleport = Ateliers.Count-1;
            Parapluie.GetComponent<CapsuleCollider>().enabled = true;
            Parapluie.GetComponent<player>().colliderParapluie.SetActive(true);
            Parapluie.GetComponent<player>().Collision = true;

        }
    }
}
