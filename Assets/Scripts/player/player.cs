using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using UnityEngine.Rendering;


public class player : MonoBehaviour
{
    
    [Header("Temporaire")]
    public GameObject parapluieFerme;
    public GameObject parapluieOuvert;

    [Header("Composants à récupérer")]
    public CanvasGroup scoreCredits;

    public CameraRotate CR;
    public GameObject VentDebug;
    public PerfectTextDisepear perfectTextD;
    public Animator ParapluieRenderer;
    public GameObject OrietationJump;
    public bool Collision = false;
    public bool fermer = false;
    Rigidbody rb;
    Animator animatorPlayer;

    private Animator animator;
    private Transform cameraTransform;
    public Slider SliderE;
    public Image SliderBG;
    public Slider SliderEnergieRW;
    public GameObject perfectIndicator;
    
    [Header("Variables changeants les controles")]

    [SerializeField] public float EnergieFlap;
    [SerializeField] private bool EnergieDown;
    [SerializeField] private float EnergieRW;    //Risk & reward, c'est l'energie qui a +/- 5 energie près donne un bonus de flap.
    [SerializeField] public float SpeedResetFlap;
    [SerializeField] public float CostFlap;
    [SerializeField] public float CostMegaFlap;
    [SerializeField] public Color ColorSliderUp;
    [SerializeField] public Color ColorSliderDown;
    [SerializeField] public float NombreFlap;
    public float FlapingNumber;
    [HideInInspector] public float DefaultForceJump;
    [SerializeField] public float ForceJump;
    //Range [ 1,5];
    [SerializeField] float ForceMegaJump;
    [SerializeField] float ForceBonusJump;
    [SerializeField] float TimerOrientation;
    [SerializeField] float TimerStabilisation;
    [SerializeField] float drag = 7;
    [SerializeField] float dragFermer = 0;
    [SerializeField] float ImpulseOrientationPlayer;
    [SerializeField] float forceOrientationAnimation;
    [SerializeField] public Vector3 OrientationVent = Vector3.zero;
    [SerializeField] public Vector3 DefaultOrientationVent=Vector3.zero;




    [SerializeField] Vector3 orientationModif;
    [SerializeField] Vector3 orientationAnim;

    private float timer = 0.3f;
    private float timerReset = 0.3f;
    public bool ActiveTimer;
    private bool onGroundFMOD = true;
    [HideInInspector] public bool onGround = false;
    [HideInInspector] public Vector3 groundPosition;
    [HideInInspector] public bool CDtpClose = false;
    private float timerCDtpClose = 2;
    private float CDtpCloseTime = 0;
    public float FlapNumberCheat;
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



    //a utiliser pour avoir la direction du parapluie
    [Header("pour Debug direction")]
    public bool forward;
    public bool backward;
    public bool left;
    public bool right;
    public bool up;
    public bool down;

    [Header("Variables ne pas changer, se fait automatiquement !!")]

    public GameObject colliderParapluie;
    public bool end = false;

    public ConvertTriggerToButton trigger;

    
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        DefaultForceJump=ForceJump;
        transform.DORotateQuaternion(Quaternion.Euler(0, 0, 0), 1);
        rb = GetComponent<Rigidbody>();
        animatorPlayer = GetComponent<Animator>();
        FlapingNumber = NombreFlap;
        cameraTransform = GameObject.Find("Main Camera").transform;
        Application.targetFrameRate = 60;
        chuteFMOD = FMODUnity.RuntimeManager.CreateInstance("event:/player/player_reaction/chute");
        flyFMOD = FMODUnity.RuntimeManager.CreateInstance("event:/player/player_reaction/fly");
        flyFMOD.start();
        //chuteFMOD.setParameterByName("Parameter 1", 0.0F);
    }

    void Update()
    {
        //currentpetit = Mathf.Lerp(currentpetit, ambiancePetit, Time.deltaTime * vitesseChangementMusique);
        //ambiance.SetParameter("ambiance petit", currentpetit);
        //currentMoyen = Mathf.Lerp(currentMoyen, ambianceMoyen, Time.deltaTime * vitesseChangementMusique);
        //ambiance.SetParameter("ambiance moyen", currentMoyen);
        //currentGrateCiel = Mathf.Lerp(currentGrateCiel, ambianceGrateCiel, Time.deltaTime * vitesseChangementMusique);
        //ambiance.SetParameter("ambiance ville", currentGrateCiel);
        //currentPetitCiel = Mathf.Lerp(currentPetitCiel, ambiancePetitCiel, Time.deltaTime * vitesseChangementMusique);
        //ambiance.SetParameter("petit ciel", currentPetitCiel);
        //currentMoyenCiel = Mathf.Lerp(currentMoyenCiel, ambianceMoyenCiel, Time.deltaTime * vitesseChangementMusique);
        //ambiance.SetParameter("moyen ciel", currentMoyenCiel);
        //currentGrateCielCiel = Mathf.Lerp(currentGrateCielCiel, ambianceGrateCielCiel, Time.deltaTime * vitesseChangementMusique);
        //ambiance.SetParameter("gratte ciel ciel", currentGrateCielCiel);
        //currentWata = Mathf.Lerp(currentWata, ambianceWata, Time.deltaTime * vitesseChangementMusique);
        //ambiance.SetParameter("wata", currentWata);
        //currentSpace = Mathf.Lerp(currentSpace, ambianceSpace, Time.deltaTime * vitesseChangementMusique);
        //ambiance.SetParameter("space", currentSpace);
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
        if (!onGround)
        {
            rb.AddForce(OrientationVent,ForceMode.Impulse);
        }
        if (end)
        {
            scoreCredits.alpha += (Time.deltaTime * 0.1f);
            return;
        }
        if (onGround)
        {
            chuteFMOD.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            flyFMOD.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            if (onGroundFMOD) FMODUnity.RuntimeManager.PlayOneShot("event:/player/player_reaction/landing");
            onGroundFMOD = false;
            transform.position = groundPosition;
            if (Input.GetAxisRaw("Horizontal") > 0f || Input.GetAxisRaw("Horizontal") < 0f || Input.GetAxisRaw("Vertical") > 0f || Input.GetAxisRaw("Vertical") < 0f)
            {
                if (Input.GetAxisRaw("Horizontal") > 0f)
                {
                    transform.RotateAround(groundPosition, -cameraTransform.forward, forceOrientationAnimation * 20 * Time.deltaTime);
                }
                else if (Input.GetAxisRaw("Horizontal") < 0f)
                {
                    transform.RotateAround(groundPosition, cameraTransform.forward, forceOrientationAnimation * 20 * Time.deltaTime);
                }
                if (Input.GetAxisRaw("Vertical") > 0f)
                {
                    transform.RotateAround(groundPosition, cameraTransform.right, forceOrientationAnimation * 20 * Time.deltaTime);
                }
                else if (Input.GetAxisRaw("Vertical") < 0f)
                {
                    transform.RotateAround(groundPosition, -cameraTransform.right, forceOrientationAnimation * 20 * Time.deltaTime);
                }
            }
        }
        
        //flap
        if (((Input.GetButtonDown("Flap")||trigger.triggerR) && FlapingNumber <= 0f) || ((Input.GetButtonDown("Flap")||trigger.triggerR) && fermer) || ((Input.GetButtonDown("Flap")||trigger.triggerR) && EnergieDown)) FMODUnity.RuntimeManager.PlayOneShot("event:/player/noflap");
        
        //Le parapluie s'ouvre si il est fermé et qu'on veut flap
        if (((Input.GetButtonDown("Flap")||trigger.triggerR) || (Input.GetButtonDown("Megaflap")||trigger.triggerL))&& !EnergieDown && fermer)
        {
            fermer = false;
            FMODUnity.RuntimeManager.PlayOneShot("event:/player/player_action/open");
            chuteFMOD.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            flyFMOD.start();
            parapluieFerme.SetActive(false);
            parapluieOuvert.SetActive(true);
        }
        
        //flap en bonus
        if ((Input.GetButtonDown("Flap")||trigger.triggerR) && /*FlapingNumber >= 1f*/ !EnergieDown && !fermer && (EnergieFlap == EnergieRW || (EnergieFlap < EnergieRW && EnergieRW - EnergieFlap <= 3) || (EnergieFlap > EnergieRW && EnergieFlap - EnergieRW <=3)))
        {
            perfectTextD.Disappear();
            //Debug.Log("Mega Flap");
            EnergieRW = EnergieFlap - CostFlap + 5f;
            onGround = false;
            onGroundFMOD = true;
            rb.AddForce((OrietationJump.transform.position - transform.position) * (ForceJump+ForceBonusJump), ForceMode.Impulse);
            parapluieFerme.SetActive(true);
            parapluieOuvert.SetActive(false);
            //ParapluieRenderer.Play("Fermeture");
            FlapingNumber = FlapingNumber - 1f;
            ActiveTimer = true;
            FMODUnity.RuntimeManager.PlayOneShot("event:/player/player_action/flap");
            flyFMOD.start();
            //Debug.Log("1");
            if (EnergieFlap <= CostFlap +5f && EnergieFlap >= CostFlap -5f) EnergieFlap = 1f;
            else EnergieFlap -= CostFlap;

            if (EnergieFlap <= 0)
            {
                EnergieFlap = 0f;
                EnergieDown = true;
                SliderBG.color = ColorSliderDown;
            }
        }
        else if ((Input.GetButtonDown("Flap")||trigger.triggerR) && /*FlapingNumber >= 1f*/ !EnergieDown && !fermer)
        {
            flap();
        }
        //Méga flap
        if (((Input.GetButtonDown("Megaflap")||trigger.triggerL) && FlapingNumber <= 0f) || ((Input.GetButtonDown("Megaflap")||trigger.triggerL) && fermer) || ((Input.GetButtonDown("Megaflap")||trigger.triggerL) && EnergieDown)) FMODUnity.RuntimeManager.PlayOneShot("event:/player/noflap");
        
        if ((Input.GetButtonDown("Megaflap")||trigger.triggerL) && /*FlapingNumber >= 1f*/ !EnergieDown && !fermer && (EnergieFlap == EnergieRW || (EnergieFlap < EnergieRW && EnergieRW - EnergieFlap <= 3) || (EnergieFlap > EnergieRW && EnergieFlap - EnergieRW <=3)))
        {
            perfectTextD.Disappear();
            EnergieRW = EnergieFlap - CostMegaFlap  + 5f;
            onGround = false;
            onGroundFMOD = true;
            rb.AddForce((OrietationJump.transform.position - transform.position) * (ForceMegaJump+ForceBonusJump), ForceMode.Impulse);
            parapluieFerme.SetActive(true);
            parapluieOuvert.SetActive(false);
            //ParapluieRenderer.Play("Fermeture");
            FlapingNumber = FlapingNumber - 1f;
            ActiveTimer = true;
            FMODUnity.RuntimeManager.PlayOneShot("event:/player/player_action/flap_perfect");
            flyFMOD.start();
            if (EnergieFlap <= CostMegaFlap +5f && EnergieFlap >= CostMegaFlap -5f) EnergieFlap = 1f;
            else EnergieFlap -= CostMegaFlap;
            timer = 1f;
            if (EnergieFlap <= 0)
            {
                EnergieFlap = 0f;
                EnergieDown = true;
                SliderBG.color = ColorSliderDown;
            }
        }
        else if ((Input.GetButtonDown("Megaflap")||trigger.triggerL) && /*FlapingNumber >= 1f*/ !EnergieDown && !fermer)
        {
            EnergieRW = EnergieFlap - CostMegaFlap + 5f;
            onGround = false;
            onGroundFMOD = true;
            rb.AddForce((OrietationJump.transform.position - transform.position) * ForceMegaJump, ForceMode.Impulse);
            parapluieFerme.SetActive(true);
            parapluieOuvert.SetActive(false);
            //ParapluieRenderer.Play("Fermeture");
            FlapingNumber = FlapingNumber - 1f;
            ActiveTimer = true;
            FMODUnity.RuntimeManager.PlayOneShot("event:/player/player_action/flap_perfect");
            flyFMOD.start();
            if (EnergieFlap <= CostMegaFlap +5f && EnergieFlap >= CostMegaFlap -5f) EnergieFlap = 1f;
            else EnergieFlap -= CostMegaFlap;
            timer = 1f;
            if (EnergieFlap <= 0)
            {
                EnergieFlap = 0f;
                EnergieDown = true;
                SliderBG.color = ColorSliderDown;
            }
        }


        if (Input.GetButtonDown("Fermeture") && ActiveTimer == false)
        {
            fermer = !fermer;
            if (fermer)
            {
                parapluieFerme.SetActive(true);
                parapluieOuvert.SetActive(false);
                FMODUnity.RuntimeManager.PlayOneShot("event:/player/player_action/close");
                chuteFMOD.start();
                flyFMOD.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            }

            else
            {
                FMODUnity.RuntimeManager.PlayOneShot("event:/player/player_action/open");
                chuteFMOD.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                flyFMOD.start();
                parapluieFerme.SetActive(false);
                parapluieOuvert.SetActive(true);
            }
        }
        if (ActiveTimer) timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            parapluieFerme.SetActive(false);
            parapluieOuvert.SetActive(true);
            timer = timerReset;
            ActiveTimer = false;
        }

        if (Input.GetButtonDown("Gainenergie"))
        {
            FlapingNumber += FlapNumberCheat;
            EnergieFlap = 100f;
        }

        if (!fermer)
        {
            EnergieFlap += SpeedResetFlap * Time.deltaTime;
        }
        else
        {
            EnergieFlap += SpeedResetFlap * 5 * Time.deltaTime;
        }
        SliderE.value = EnergieFlap;
        if (EnergieFlap >= 100)
        {
            EnergieDown = false;
            SliderBG.color = ColorSliderUp;
            EnergieFlap = 100f;
            EnergieRW = 100f;
        }

        if (EnergieDown) EnergieRW = EnergieFlap;

        SliderEnergieRW.value = EnergieRW;
        
        if (!EnergieDown && (EnergieFlap == EnergieRW || (EnergieFlap < EnergieRW && EnergieRW - EnergieFlap <= 3) || (EnergieFlap > EnergieRW && EnergieFlap - EnergieRW <= 3))) perfectIndicator.SetActive(true);
        else perfectIndicator.SetActive(false);



    }

    void FixedUpdate()
    {
        if (end) return;
        orientationModif = OrientationVent + ((cameraTransform.right * Input.GetAxis("Horizontal") + cameraTransform.forward * Input.GetAxis("Vertical")) * ImpulseOrientationPlayer);
        orientationAnim = OrientationVent + cameraTransform.right * Input.GetAxis("Horizontal") + cameraTransform.forward * Input.GetAxis("Vertical");

        //Debug.Log(gameObject.transform.rotation.eulerAngles.x);

        //la rotation du parapluie le fait chuter

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
                    rb.AddTorque(-cameraTransform.forward * forceOrientationAnimation, ForceMode.Acceleration);
                }
                else if (Input.GetAxisRaw("Horizontal") < 0f)
                {
                    rb.AddTorque(cameraTransform.forward * forceOrientationAnimation, ForceMode.Acceleration);
                }
                if (Input.GetAxisRaw("Vertical") > 0f)
                {
                    rb.AddTorque(cameraTransform.right * forceOrientationAnimation, ForceMode.Acceleration);
                }
                else if (Input.GetAxisRaw("Vertical") < 0f)
                {
                    rb.AddTorque(-cameraTransform.right * forceOrientationAnimation, ForceMode.Acceleration);
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
        colliderParapluie = other.gameObject;
        FMODUnity.RuntimeManager.PlayOneShot("event:/player/player_reaction/landing");
    }
    private void OnCollisionStay(Collision other)
    {

    }

    private void OnCollisionExit(Collision other)
    {
        Collision = false;
        //transform.DORotateQuaternion(Quaternion.Euler(0, 0, 0), 1);
    }

    public void DisableAnimator()
    {
        animator.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("End"))
        {
            CR.CameraControl = 1;
            end = true;
            //gameObject.GetComponent<Rigidbody>().useGravity = false;
            gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
            parapluieFerme.SetActive(true);
            parapluieOuvert.SetActive(false);
            VentDebug.gameObject.SetActive(true);
            fermer = false;
        }
    }

    public void flap()
    {
        EnergieRW = EnergieFlap - CostFlap + 5f;
        onGround = false;
        onGroundFMOD = true;
        rb.AddForce((OrietationJump.transform.position - transform.position) * ForceJump, ForceMode.Impulse);
        parapluieFerme.SetActive(true);
        parapluieOuvert.SetActive(false);
        //ParapluieRenderer.Play("Fermeture");
        FlapingNumber = FlapingNumber - 1f;
        ActiveTimer = true;
        FMODUnity.RuntimeManager.PlayOneShot("event:/player/player_action/flap");
        flyFMOD.start();
        //Debug.Log("2");
        if (EnergieFlap <= CostFlap +5f && EnergieFlap >= CostFlap -5f) EnergieFlap = 1f;
        else EnergieFlap -= CostFlap;

        if (EnergieFlap <= 0)
        {
            EnergieFlap = 0f;
            EnergieDown = true;
            SliderBG.color = ColorSliderDown;
        }
    }
}
