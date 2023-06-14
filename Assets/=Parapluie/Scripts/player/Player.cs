using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.Events;
using UnityEngine.Rendering;
using UnityEngine.Serialization;


public class Player : MonoBehaviour
{
    [Header("Temporaire // Les fonctionnements à améliorer")]
    
    public GameObject parapluieFerme;
    public GameObject parapluieOuvert;

    [Header("Composants à récupérer")]
    
    public ConvertTriggerToButton triggerConvertToButton;
    public CameraRotate CR;
    public Transform CameraTransform;
    public GameObject OrietationJump;
    
    public bool Collision = false;
    public bool fermer = false;

    private Rigidbody rb;

    [Header("Variables changeants les controles")]

    public float EnergieFlap;
    public bool EnergieDown;
    public float EnergieRW;    //Risk & reward, c'est l'energie qui a +/- 5 energie près donne un bonus de flap.
    public float SpeedResetFlap;
    public float CostFlap;
    public float CostMegaFlap;
    [HideInInspector] public float DefaultForceJump;
    [SerializeField] public float ForceJump;
    [SerializeField] float ForceMegaJump;
    [SerializeField] float ForceBonusJump;
    [SerializeField] float drag = 7;
    [SerializeField] float dragFermer = 0;
    [SerializeField] float ImpulseOrientationPlayer;
    [SerializeField] float forceOrientationAnimation;
    [SerializeField] public Vector3 OrientationVent = Vector3.zero;
    [SerializeField] public Vector3 DefaultOrientationVent=Vector3.zero;
    
    //je ne sais pas à quoi ca sert mais ca à l'air de servir
    [SerializeField] Vector3 orientationModif;
    [SerializeField] Vector3 orientationAnim;

    private float timer = 0.3f;
    private float timerReset = 0.3f;
    public bool ActiveTimer;
    [SerializeField]  private bool onGroundFMOD = true;
    [HideInInspector] public bool onGround = false;
    [HideInInspector] public Vector3 groundPosition;
    [HideInInspector] public bool CDtpClose = false;
    private float timerCDtpClose = 2;
    private float CDtpCloseTime = 0;

    //a utiliser pour avoir la direction du Parapluie
    [Header("pour Debug direction")]
    public bool forward;
    public bool backward;
    public bool left;
    public bool right;
    public bool up;
    public bool down;

    [Header("Variables ne pas changer, se fait automatiquement !!")]

    public GameObject colliderContactParapluie;
    public bool DisableMove = false;
    private FMOD.Studio.EventInstance chuteFMOD;
    private FMOD.Studio.EventInstance flyFMOD;
    [SerializeField] float vitesseChangementMusique=1.5f;
    [SerializeField] FMODUnity.StudioEventEmitter ambiance;
    [HideInInspector] public float ambiancePetit=0;
    float currentpetit = 0;
    [HideInInspector] public float ambianceMoyen = 0;
    float currentMoyen = 0;
    [HideInInspector] public float ambianceGrateCiel = 0;
    float currentGrateCiel = 0;
    [HideInInspector] public float ambiancePetitCiel = 0;
    float currentPetitCiel = 0;
    [HideInInspector] public float ambianceMoyenCiel = 0;
    float currentMoyenCiel = 0;
    [HideInInspector] public float ambianceGrateCielCiel = 0;
    float currentGrateCielCiel = 0;
    [HideInInspector] public float ambianceWata = 0;
    float currentWata = 0;
    [HideInInspector] public float ambianceSpace = 0;
    float currentSpace = 0;
    
    // Events
    [Header("Flaps Signes & FeedBack à mettre")]
    public UnityEvent OnFlap = new UnityEvent();
    public UnityEvent OnPerfectFlap = new UnityEvent();
    public UnityEvent OnMegaFlap = new UnityEvent();
    public UnityEvent OnMegaPerfectFlap = new UnityEvent();
    
    void Start()
    {
        DefaultForceJump=ForceJump;
        transform.DORotateQuaternion(Quaternion.Euler(0, 0, 0), 1);
        rb = GetComponent<Rigidbody>();
        Application.targetFrameRate = 60;
        chuteFMOD = FMODUnity.RuntimeManager.CreateInstance("event:/Parapluie/player_reaction/chute");
        flyFMOD = FMODUnity.RuntimeManager.CreateInstance("event:/Parapluie/player_reaction/fly");
        flyFMOD.start();
    }

    void Update()
    {
        currentpetit = Mathf.Lerp(currentpetit, ambiancePetit, Time.deltaTime * vitesseChangementMusique);
        ambiance.SetParameter("ambiance petit", currentpetit);
        currentMoyen = Mathf.Lerp(currentMoyen, ambianceMoyen, Time.deltaTime * vitesseChangementMusique);
        ambiance.SetParameter("ambiance moyen", currentMoyen);
        currentGrateCiel = Mathf.Lerp(currentGrateCiel, ambianceGrateCiel, Time.deltaTime * vitesseChangementMusique);
        ambiance.SetParameter("ambiance ville", currentGrateCiel);
        currentPetitCiel = Mathf.Lerp(currentPetitCiel, ambiancePetitCiel, Time.deltaTime * vitesseChangementMusique);
        ambiance.SetParameter("petit ciel", currentPetitCiel);
        currentMoyenCiel = Mathf.Lerp(currentMoyenCiel, ambianceMoyenCiel, Time.deltaTime * vitesseChangementMusique);
        ambiance.SetParameter("moyen ciel", currentMoyenCiel);
        currentGrateCielCiel = Mathf.Lerp(currentGrateCielCiel, ambianceGrateCielCiel, Time.deltaTime * vitesseChangementMusique);
        ambiance.SetParameter("gratte ciel ciel", currentGrateCielCiel);
        currentWata = Mathf.Lerp(currentWata, ambianceWata, Time.deltaTime * vitesseChangementMusique);
        ambiance.SetParameter("wata", currentWata);
        currentSpace = Mathf.Lerp(currentSpace, ambianceSpace, Time.deltaTime * vitesseChangementMusique);
        ambiance.SetParameter("space", currentSpace);
        //ne bloque pas le Parapluie quand tu tp
        
        if (CDtpClose)
        {
            if (CDtpCloseTime < timerCDtpClose)
            {
                CDtpCloseTime += Time.deltaTime;
                fermer = false;
            }
            else
            {
                CDtpClose = false;
                CDtpCloseTime = 0;
            }
        }
        
        //desactive les controles
        if (DisableMove) return;
        
        
        if (!onGround)
        {
            rb.AddForce(OrientationVent,ForceMode.Impulse);
        }
        if (DisableMove)
        {
            return;
        }
        if (onGround)
        {
            chuteFMOD.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            flyFMOD.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            if (onGroundFMOD) FMODUnity.RuntimeManager.PlayOneShot("event:/Parapluie/player_reaction/landing");
            onGroundFMOD = false;
            transform.position = groundPosition;
            if (Input.GetAxisRaw("Horizontal") > 0f || Input.GetAxisRaw("Horizontal") < 0f || Input.GetAxisRaw("Vertical") > 0f || Input.GetAxisRaw("Vertical") < 0f)
            {
                if (Input.GetAxisRaw("Horizontal") > 0f)
                {
                    transform.RotateAround(groundPosition, -CameraTransform.forward, forceOrientationAnimation * 20 * Time.deltaTime);
                }
                else if (Input.GetAxisRaw("Horizontal") < 0f)
                {
                    transform.RotateAround(groundPosition, CameraTransform.forward, forceOrientationAnimation * 20 * Time.deltaTime);
                }
                if (Input.GetAxisRaw("Vertical") > 0f)
                {
                    transform.RotateAround(groundPosition, CameraTransform.right, forceOrientationAnimation * 20 * Time.deltaTime);
                }
                else if (Input.GetAxisRaw("Vertical") < 0f)
                {
                    transform.RotateAround(groundPosition, -CameraTransform.right, forceOrientationAnimation * 20 * Time.deltaTime);
                }
            }
        }

        
        
        
        
        
        //Le Parapluie s'ouvre si il est fermé et qu'on veut flap on mega flap
        if (((Input.GetButtonDown("Flap")||triggerConvertToButton.triggerR) || (Input.GetButtonDown("Megaflap")||triggerConvertToButton.triggerL))&& !EnergieDown && fermer)
        {
            fermer = false;
            FMODUnity.RuntimeManager.PlayOneShot("event:/Parapluie/player_action/open");
            chuteFMOD.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            flyFMOD.start();
            parapluieFerme.SetActive(false);
            parapluieOuvert.SetActive(true);
        }
        
        
        //ne peut pas flap
        else if (((Input.GetButtonDown("Flap")||triggerConvertToButton.triggerR) && fermer) || ((Input.GetButtonDown("Flap")||triggerConvertToButton.triggerR) && EnergieDown)) FMODUnity.RuntimeManager.PlayOneShot("event:/Parapluie/noflap");
        
        //flap
        if ((Input.GetButtonDown("Flap")||triggerConvertToButton.triggerR) && !EnergieDown && !fermer)
        {
            Flap();
        }
        
        //ne peut pas mega flap
        if (((Input.GetButtonDown("Megaflap")||triggerConvertToButton.triggerL) && fermer) || ((Input.GetButtonDown("Megaflap")||triggerConvertToButton.triggerL) && EnergieDown)) FMODUnity.RuntimeManager.PlayOneShot("event:/Parapluie/noflap");
        
        //Méga flap
        if ((Input.GetButtonDown("Megaflap")||triggerConvertToButton.triggerL) &&!EnergieDown && !fermer)
        {
            MegaFlap();
        }


        if (Input.GetButtonDown("Fermeture") && ActiveTimer == false)
        {
            Fermeture();
        }
        
        
        if (ActiveTimer) timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            parapluieFerme.SetActive(false);
            parapluieOuvert.SetActive(true);
            timer = timerReset;
            ActiveTimer = false;
        }
        

        if (!fermer)
        {
            EnergieFlap += SpeedResetFlap * Time.deltaTime;
        }
        else
        {
            EnergieFlap += SpeedResetFlap * 5 * Time.deltaTime;
        }
        if (EnergieFlap >= 100)
        {
            EnergieDown = false;
            
            EnergieFlap = 100f;
            EnergieRW = 98f;
        }

        if (EnergieDown && EnergieRW <= 98f) EnergieRW = EnergieFlap;

        if (EnergieRW >= 98f) EnergieRW = 98f;
    }

    void FixedUpdate()
    {
        //desactive les controles
        if (DisableMove) return;
        
        
        orientationModif = OrientationVent + ((CameraTransform.right * Input.GetAxis("Horizontal") + CameraTransform.forward * Input.GetAxis("Vertical")) * ImpulseOrientationPlayer);
        orientationAnim = OrientationVent + CameraTransform.right * Input.GetAxis("Horizontal") + CameraTransform.forward * Input.GetAxis("Vertical");

        
        //la rotation du Parapluie le fait chuter
        if (ActiveTimer) rb.drag = drag;
        else if (fermer)
        {
            rb.drag = dragFermer;
        }
        else if ((gameObject.transform.rotation.eulerAngles.x >= 40f && gameObject.transform.rotation.eulerAngles.x <= 320f) || (gameObject.transform.rotation.eulerAngles.z >= 40f && gameObject.transform.rotation.eulerAngles.z <= 320f) /*|| gameObject.transform.localRotation.eulerAngles.z >= 40f || gameObject.transform.localRotation.eulerAngles.z <= -40f*/)
        {
            rb.drag = drag / 2;
        }
        else
        {
            rb.drag = drag;
            //rb.AddForce(orientationModif, ForceMode.Impulse);
        }

        if (Collision)
        {
            rb.freezeRotation = false;
            //FlapingNumber = NombreFlap;
        }
        else
        {
            rb.freezeRotation = true;
            if (Input.GetAxisRaw("Horizontal") > 0f || Input.GetAxisRaw("Horizontal") < 0f || Input.GetAxisRaw("Vertical") > 0f || Input.GetAxisRaw("Vertical") < 0f)
            {
                if (Input.GetAxisRaw("Horizontal") > 0f)
                {
                    rb.AddTorque(-CameraTransform.forward * forceOrientationAnimation, ForceMode.Acceleration);
                }
                else if (Input.GetAxisRaw("Horizontal") < 0f)
                {
                    rb.AddTorque(CameraTransform.forward * forceOrientationAnimation, ForceMode.Acceleration);
                }
                if (Input.GetAxisRaw("Vertical") > 0f)
                {
                    rb.AddTorque(CameraTransform.right * forceOrientationAnimation, ForceMode.Acceleration);
                }
                else if (Input.GetAxisRaw("Vertical") < 0f)
                {
                    rb.AddTorque(-CameraTransform.right * forceOrientationAnimation, ForceMode.Acceleration);
                }
            }
            else
            {
                rb.angularVelocity = Vector3.zero;
            }
        }
    }
    
    private void OnCollisionEnter(Collision other)
    {
        Collision = true;
        colliderContactParapluie = other.gameObject;
    }

    private void OnCollisionExit(Collision other)
    {
        Collision = false;
        //transform.DORotateQuaternion(Quaternion.Euler(0, 0, 0), 1);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("End"))
        {
            CR.CameraControl = 1;
            DisableMove = true;
            //gameObject.GetComponent<Rigidbody>().useGravity = false;
            gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
            parapluieFerme.SetActive(true);
            parapluieOuvert.SetActive(false);
            fermer = false;
        }
    }
    
    public void Flap()
    {
        //perfect?
        if ((EnergieFlap == EnergieRW || (EnergieFlap < EnergieRW && EnergieRW - EnergieFlap <= 3) ||
             (EnergieFlap > EnergieRW && EnergieFlap - EnergieRW <= 3)))
        {
            rb.AddForce((OrietationJump.transform.position - transform.position) * (ForceJump+ForceBonusJump), ForceMode.Impulse);
            FMODUnity.RuntimeManager.PlayOneShot("event:/Parapluie/player_action/flap_perfect");
            OnPerfectFlap.Invoke();
        }
        else 
        {
            OnFlap.Invoke();
            rb.AddForce((OrietationJump.transform.position - transform.position) * ForceJump, ForceMode.Impulse);
        }
        EnergieRW = EnergieFlap - CostFlap + 5f;
        onGround = false;
        onGroundFMOD = true;
        
        parapluieFerme.SetActive(true);
        parapluieOuvert.SetActive(false);
        ActiveTimer = true;
        FMODUnity.RuntimeManager.PlayOneShot("event:/Parapluie/player_action/flap");
        flyFMOD.start();
        if (EnergieFlap <= CostFlap +5f && EnergieFlap >= CostFlap -5f) EnergieFlap = 1f;
        else EnergieFlap -= CostFlap;

        if (EnergieFlap <= 0)
        {
            EnergieFlap = 0f;
            EnergieDown = true;
        }
    }

    public void MegaFlap()
    {
        //perfect?
        if ((EnergieFlap == EnergieRW || (EnergieFlap < EnergieRW && EnergieRW - EnergieFlap <= 3) ||
             (EnergieFlap > EnergieRW && EnergieFlap - EnergieRW <= 3)))
        {
            rb.AddForce((OrietationJump.transform.position - transform.position) * (ForceMegaJump+ForceBonusJump), ForceMode.Impulse);
            FMODUnity.RuntimeManager.PlayOneShot("event:/Parapluie/player_action/perfect_puissant");
            OnMegaPerfectFlap.Invoke();
        }
        else
        {
            rb.AddForce((OrietationJump.transform.position - transform.position) * ForceMegaJump, ForceMode.Impulse);
            OnMegaFlap.Invoke();
        }

        EnergieRW = EnergieFlap - CostMegaFlap  + 5f;
        onGround = false;
        onGroundFMOD = true;
        parapluieFerme.SetActive(true);
        parapluieOuvert.SetActive(false);
        ActiveTimer = true;
        FMODUnity.RuntimeManager.PlayOneShot("event:/Parapluie/player_action/flap_perfect");
        flyFMOD.start();
        if (EnergieFlap <= CostMegaFlap +5f && EnergieFlap >= CostMegaFlap -5f) EnergieFlap = 1f;
        else EnergieFlap -= CostMegaFlap;
        timer = 0.7f;
        if (EnergieFlap <= 0)
        {
            EnergieFlap = 0f;
            EnergieDown = true;
        }
        
        OnMegaFlap.Invoke();
    }

    public void Fermeture()
    {
        fermer = !fermer;
        if (fermer)
        {
            parapluieFerme.SetActive(true);
            parapluieOuvert.SetActive(false);
            FMODUnity.RuntimeManager.PlayOneShot("event:/Parapluie/player_action/close");
            chuteFMOD.start();
            flyFMOD.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }

        else
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/Parapluie/player_action/open");
            chuteFMOD.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            flyFMOD.start();
            parapluieFerme.SetActive(false);
            parapluieOuvert.SetActive(true);
        }
        
    }
    
}