using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vent : MonoBehaviour
{
    private MeshRenderer MR;
    [SerializeField] public Vector3 ajoutVent;
    private Vector3 vent;
    [SerializeField] GameObject player;
    [SerializeField] player PlayerScript;
    [SerializeField] private float timerImpulseVent;
    [SerializeField] float timerImpulseVentMin, timerImpulseVentMax;
    [SerializeField] private float timerResetValue = 0.5f;
    [SerializeField] float timerResetValueMin, timerResetValueMax;
    [SerializeField] bool ventContinue = false;
    [SerializeField] private bool directionLocal = false;

    [SerializeField] float multiplicateurFlap;
    [SerializeField] float multiplicateurVent;
    bool addedForward = false;
    bool addedright = false;
    bool addedup = false;

    float timer;
    float timerReset = 0;

    private void Start()
    {
        MR = GetComponent<MeshRenderer>();
        //MR.enabled = false;
        timerImpulseVent = Random.Range(timerImpulseVentMin, timerImpulseVentMax);
        timerResetValue = Random.Range(timerResetValueMin, timerResetValueMax);
        vent = ajoutVent;
    }

    private void OnTriggerStay(Collider other)
    {
        print(PlayerScript.OrientationVent);
        
        if (other.CompareTag("Player"))
        {
            //determine la puissance du vent en fonction de l'orientation du parapluie
            if (ajoutVent.y > 0)
            {
                if (PlayerScript.up)
                {
                    PlayerScript.ForceJump = PlayerScript.DefaultForceJump * multiplicateurFlap;
                }
                else if (PlayerScript.down)
                {

                }
            }
            else if (ajoutVent.y < 0)
            {
                if (PlayerScript.down)
                {

                }
                else if (PlayerScript.up)
                {
                    PlayerScript.ForceJump = PlayerScript.DefaultForceJump / multiplicateurFlap;
                }
            }

            if (ajoutVent.x > 0)
            {
                if (PlayerScript.right)
                {
                    vent.x = ajoutVent.x * multiplicateurVent;
                }
                else if (PlayerScript.left)
                {
                    vent.z = ajoutVent.z / multiplicateurVent;
                }
            }
            else if (ajoutVent.x < 0)
            {
                if (PlayerScript.left)
                {
                    vent.x = ajoutVent.x * multiplicateurVent;
                }
                else if (PlayerScript.right)
                {
                    vent.z = ajoutVent.z / multiplicateurVent;
                }
            }

            if (ajoutVent.z > 0)
            {
                if (PlayerScript.forward)
                {
                    vent.z = ajoutVent.z * multiplicateurVent;
                }
                else if (PlayerScript.backward)
                {
                    vent.z = ajoutVent.z / multiplicateurVent;
                }
            }
            else if (ajoutVent.z < 0)
            {
                if (PlayerScript.backward)
                {
                    vent.z = ajoutVent.z * multiplicateurVent;
                }
                else if (PlayerScript.forward)
                {
                    vent.z = ajoutVent.z / multiplicateurVent;
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
                    if (vent.z != 0)
                    {
                        if (!addedForward)
                        {
                            PlayerScript.OrientationVent += transform.forward * vent.z;
                            addedForward = true;
                        }
                        
                    }else if (vent.z < 0)
                    
                    if (vent.x != 0)
                    {
                            if (!addedright)
                            {
                                PlayerScript.OrientationVent += transform.right * vent.z;
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
                            if (!addedright)
                            {
                                PlayerScript.OrientationVent += transform.right * vent.z;
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
        vent = ajoutVent;
        PlayerScript.OrientationVent = PlayerScript.DefaultOrientationVent;
        PlayerScript.ForceJump = PlayerScript.DefaultForceJump;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + ajoutVent *50);
    }
}
