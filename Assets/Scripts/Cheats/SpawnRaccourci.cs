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
    
    [Header("Les Bonus qui donnent les tp")]
    public Transform _22Biphosgate;
    public Transform _Bridge;
    public Transform _Scalpel;
    public Transform _Heron;
    public Transform _Gherkin;
    public Transform _TheShard;
    public Transform _Talkie;
    public Transform _Stadium;
    public PauseMenu pm;

    private void Start()
    {
        Ateliers = GetComponentsInChildren<Transform>().ToList();
        Ateliers.RemoveAt(0);
    }

    void Update()
    {
        //if( Input.GetKeyDown(KeyCode.F5)) SceneManager.LoadScene("test");
        //téléportations aux points de la liste
        if(Input.GetButtonDown("CheatSpawn") && pm.canCheat && !pm.isMenu)
        {
            Parapluie.GetComponent<player>().flap();
            Parapluie.GetComponent<CapsuleCollider>().enabled = false;
            //Parapluie.GetComponent<player>().colliderParapluie.SetActive(false);
            Parapluie.GetComponent<player>().Collision = false;
            Parapluie.transform.position = Ateliers[AtelierTeleport].transform.position;
            AtelierTeleport += 1;
            Parapluie.GetComponent<player>().FlapingNumber = Parapluie.GetComponent<player>().NombreFlap;
            if(AtelierTeleport > Ateliers.Count-1) AtelierTeleport = 0;
            //Parapluie.GetComponent<player>().Collision = true;
            //Parapluie.GetComponent<player>().colliderParapluie.SetActive(true);
            Parapluie.GetComponent<CapsuleCollider>().enabled = true;
            //Parapluie.GetComponent<player>().CDtpClose = true;
        }
        /*
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

        }*/
    }
    public void _22BiphopsgateTP()
    {
        Teleport(_22Biphosgate);
    }

    public void _BridgeTP()
    {
        Teleport(_Bridge);
    }
    public void _ScalpelTP()
    {
        Teleport(_Scalpel);
    }
    public void _HeronTP()
    {
        Teleport(_Heron);
    }
    public void _GherkinTP()
    {
        Teleport(_Gherkin);
    }
    public void _TheShardTP()
    {
        Teleport(_TheShard);
    }
    public void _TalkieTP()
    {
        Teleport(_Talkie);
    }
    public void _StadiumTP()
    {
        Teleport(_Stadium);
    }





    private void Teleport(Transform T)
    {
        Debug.Log(T);
        //Parapluie.transform.Translate(T.position,Space.Self);
        Parapluie.GetComponent<player>().flap();
        Parapluie.GetComponent<CapsuleCollider>().enabled = false;
        Parapluie.GetComponent<player>().colliderParapluie.SetActive(false);
        Parapluie.GetComponent<player>().Collision = false;
        Parapluie.transform.position = T.transform.position;
        Parapluie.GetComponent<player>().FlapingNumber = Parapluie.GetComponent<player>().NombreFlap;
        Parapluie.GetComponent<player>().Collision = true;
        Parapluie.GetComponent<player>().colliderParapluie.SetActive(true);
        Parapluie.GetComponent<CapsuleCollider>().enabled = true;
    }
}
