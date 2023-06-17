using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Vent : MonoBehaviour
{
    private MeshRenderer MR;
    [SerializeField] private float force;
    [SerializeField] private Vector3 vent;
    [SerializeField] GameObject player;
    [SerializeField] Player PlayerScript;
    [SerializeField] private float timerImpulseVent;
    [SerializeField] float timerImpulseVentMin, timerImpulseVentMax;
    [SerializeField] private float timerResetValue = 0.5f;
    [SerializeField] float timerResetValueMin, timerResetValueMax;
    [SerializeField] bool ventContinue = false;
    [SerializeField] private bool directionLocal = false;

    [SerializeField] float multiplicateurFlap;
    [SerializeField] float multiplicateurVent;
    [SerializeField] bool addedForward = false;
    [SerializeField] bool addedright = false;
    [SerializeField] bool addedup = false;
    Vector3 ventLocalDirection;

    float timer;
    float timerReset = 0;

    [HideInInspector] public Vector3 AjoutVent;

    private WindRendererParameters windParticles;

    private void Start()
    {
        MR = GetComponent<MeshRenderer>();
        MR.enabled = false;
        timerImpulseVent = Random.Range(timerImpulseVentMin, timerImpulseVentMax);
        timerResetValue = Random.Range(timerResetValueMin, timerResetValueMax);
        vent = AjoutVent;

        AjoutVent = transform.forward * force;

        windParticles = GetComponent<WindRendererParameters>();
    }

    private void OnTriggerStay(Collider other)
    {
        //print(PlayerScript.OrientationVent);
        
        if (other.CompareTag("Player"))
        {
            //determine la puissance du vent en fonction de l'orientation du Parapluie
            if (AjoutVent.y > 0)
            {
                if (PlayerScript.up)
                {
                    PlayerScript.ForceJump = PlayerScript.DefaultForceJump * multiplicateurFlap;
                }
                else if (PlayerScript.down)
                {

                }
            }
            else if (AjoutVent.y < 0)
            {
                if (PlayerScript.down)
                {

                }
                else if (PlayerScript.up)
                {
                    PlayerScript.ForceJump = PlayerScript.DefaultForceJump / multiplicateurFlap;
                }
            }

            if (AjoutVent.x > 0)
            {
                if (PlayerScript.right)
                {
                    vent.x = AjoutVent.x * multiplicateurVent;
                }
                else if (PlayerScript.left)
                {
                    vent.z = AjoutVent.z / multiplicateurVent;
                }
            }
            else if (AjoutVent.x < 0)
            {
                if (PlayerScript.left)
                {
                    vent.x = AjoutVent.x * multiplicateurVent;
                }
                else if (PlayerScript.right)
                {
                    vent.z = AjoutVent.z / multiplicateurVent;
                }
            }

            if (AjoutVent.z > 0)
            {
                if (PlayerScript.forward)
                {
                    vent.z = AjoutVent.z * multiplicateurVent;
                }
                else if (PlayerScript.backward)
                {
                    vent.z = AjoutVent.z / multiplicateurVent;
                }
            }
            else if (AjoutVent.z < 0)
            {
                if (PlayerScript.backward)
                {
                    vent.z = AjoutVent.z * multiplicateurVent;
                }
                else if (PlayerScript.forward)
                {
                    vent.z = AjoutVent.z / multiplicateurVent;
                }
            }
            //applique une force continue dans l'orientation du vent
            if (!directionLocal)
            {
                if (ventContinue)
                {

                    PlayerScript.OrientationVent = vent;
                }
                //applique une force tout les x temps pendant y secondes
                else
                {
                    if (timer < timerImpulseVent)
                    {
                        PlayerScript.OrientationVent = PlayerScript.DefaultOrientationVent;
                        timer += Time.deltaTime;
                    }
                    else
                    {
                        PlayerScript.OrientationVent = vent;
                        if (timerReset < timerResetValue)
                        {
                            timerReset += Time.deltaTime;
                        }
                        else
                        {
                            timerReset = 0;
                            timer = 0;
                            timerImpulseVent = Random.Range(timerImpulseVentMin, timerImpulseVentMax);
                            timerResetValue = Random.Range(timerResetValueMin, timerResetValueMax);
                        }

                    }
                }
            }
            else
            {
                if (ventContinue)
                {
                    ventLocalDirection = (transform.right * vent.x) + (transform.forward * vent.z) + (transform.up * vent.y);
                    PlayerScript.OrientationVent = ventLocalDirection;
                }
                else
                {
                    if (timer < timerImpulseVent)
                    {
                        PlayerScript.OrientationVent = PlayerScript.DefaultOrientationVent;
                        timer += Time.deltaTime;
                    }
                    else
                    {
                        if (vent.z != 0)
                        {
                            if (!addedForward)
                            {
                                PlayerScript.OrientationVent += transform.forward * vent.z;
                                addedForward = true;
                            }
                            
                        }
                        if (vent.x != 0)
                        {
                            print("test1");
                            if (!addedright)
                            {
                                print("test2");
                                PlayerScript.OrientationVent += transform.right * vent.x;
                                addedright = true;
                            }
                            
                        }

                        if (vent.y != 0)
                        {
                            if (!addedup)
                            {
                                PlayerScript.OrientationVent += transform.up * vent.y;
                                addedup = true;
                            }
                            
                        }
                        
                        if (timerReset < timerResetValue)
                        {
                            timerReset += Time.deltaTime;
                        }
                        else
                        {
                            timerReset = 0;
                            timer = 0;
                            timerImpulseVent = Random.Range(timerImpulseVentMin, timerImpulseVentMax);
                            timerResetValue = Random.Range(timerResetValueMin, timerResetValueMax);
                        }
                        
                    }
                }
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        addedForward = false;
        addedright = false;
        addedup = false;
        vent = AjoutVent;
        PlayerScript.OrientationVent = PlayerScript.DefaultOrientationVent;
        PlayerScript.ForceJump = PlayerScript.DefaultForceJump;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawRay(transform.position, transform.forward * force * 10);
        Gizmos.color = Color.white;
    }

    private void OnValidate()
    {
        if (windParticles == null) windParticles = GetComponent<WindRendererParameters>();

        windParticles.Force = force;
    }
}
