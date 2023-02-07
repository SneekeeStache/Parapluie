using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;


public class player : MonoBehaviour
{
    [Header("Temporaire")]
    public GameObject parapluieFerme;
    public GameObject parapluieOuvert;

    [Header("Composants à récupérer")]

    public Animator ParapluieRenderer;
    public GameObject OrietationJump;
    bool Collision = false;
    public bool fermer = false;
    Rigidbody rb;
    Animator animatorPlayer;

    private Transform cameraTransform;
    public Slider SliderE;
    public Image SliderBG;

    [Header("Variables changeants les controles")]

    [SerializeField] public float EnergieFlap;
    [SerializeField] private bool EnergieDown;
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
    public float FlapNumberCheat;
    private FMOD.Studio.EventInstance chuteFMOD;
    private FMOD.Studio.EventInstance flyFMOD;

//a utiliser pour avoir la direction du parapluie
[Header("pour Debug direction")]
    public bool forward;
    public bool backward;
    public bool left;
    public bool right;
    public bool up;
    public bool down;

    void Start()
    {
        DefaultForceJump=ForceJump;
        transform.DORotateQuaternion(Quaternion.Euler(0, 0, 0), 1);
        rb = GetComponent<Rigidbody>();
        animatorPlayer = GetComponent<Animator>();
        FlapingNumber = NombreFlap;
        cameraTransform = GameObject.Find("Main Camera").transform;
        Application.targetFrameRate = 60;
        //FMODUnity.RuntimeManager.PlayOneShot("event:/player/fly");
        //FMODUnity.RuntimeManager.PlayOneShot("event:/player/chute");
        chuteFMOD = FMODUnity.RuntimeManager.CreateInstance("event:/player/chute");
        flyFMOD = FMODUnity.RuntimeManager.CreateInstance("event:/player/fly");
        flyFMOD.start();
        //chuteFMOD.setParameterByName("Parameter 1", 0.0F);
    }

    void Update()
    {
        Debug.Log(ForceJump);
        rb.AddForce(OrientationVent,ForceMode.Impulse);
        if (onGround)
        {
            chuteFMOD.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            flyFMOD.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            //FMODUnity.RuntimeManager.StudioSystem.setParameterByName("chute", 0);
            //FMODUnity.RuntimeManager.StudioSystem.setParameterByName("fly", 0);
            if (onGroundFMOD) FMODUnity.RuntimeManager.PlayOneShot("event:/player/landing");
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
        if ((Input.GetButtonDown("Jump") && FlapingNumber <= 0f) || (Input.GetButtonDown("Jump") && fermer) || (Input.GetButtonDown("Jump") && EnergieDown)) FMODUnity.RuntimeManager.PlayOneShot("event:/player/noflap");
        if (Input.GetButtonDown("Jump") && /*FlapingNumber >= 1f*/ !EnergieDown && !fermer)
        {
            onGround = false;
            onGroundFMOD = true;
            rb.AddForce((OrietationJump.transform.position - transform.position) * ForceJump, ForceMode.Impulse);
            parapluieFerme.SetActive(true);
            parapluieOuvert.SetActive(false);
            //ParapluieRenderer.Play("Fermeture");
            FlapingNumber = FlapingNumber - 1f;
            ActiveTimer = true;
            FMODUnity.RuntimeManager.PlayOneShot("event:/player/flap");
            flyFMOD.start();
            if (EnergieFlap <= 25f && EnergieFlap >= 20f) EnergieFlap = 1f;
            else EnergieFlap -= CostFlap;

            if (EnergieFlap <= 0)
            {
                EnergieFlap = 0f;
                EnergieDown = true;
                SliderBG.color = ColorSliderDown;
            }
        }
        //Méga flap
        if ((Input.GetButtonDown("Fire4") && FlapingNumber <= 0f) || (Input.GetButtonDown("Fire4") && fermer) || (Input.GetButtonDown("Fire4") && EnergieDown)) FMODUnity.RuntimeManager.PlayOneShot("event:/player/noflap");
        if (Input.GetButtonDown("Fire4") && /*FlapingNumber >= 1f*/ !EnergieDown && !fermer)
        {
            onGround = false;
            onGroundFMOD = true;
            rb.AddForce((OrietationJump.transform.position - transform.position) * ForceMegaJump, ForceMode.Impulse);
            parapluieFerme.SetActive(true);
            parapluieOuvert.SetActive(false);
            //ParapluieRenderer.Play("Fermeture");
            FlapingNumber = FlapingNumber - 1f;
            ActiveTimer = true;
            FMODUnity.RuntimeManager.PlayOneShot("event:/player/flap");
            flyFMOD.start();
            if (EnergieFlap <= 55f && EnergieFlap >= 45f) EnergieFlap = 1f;
            else EnergieFlap -= CostMegaFlap;
            timer = 1f;
            if (EnergieFlap <= 0)
            {
                EnergieFlap = 0f;
                EnergieDown = true;
                SliderBG.color = ColorSliderDown;
            }
        }


        if (Input.GetButtonDown("Fire1") && ActiveTimer == false)
        {
            fermer = !fermer;
            if (fermer)
            {
                parapluieFerme.SetActive(true);
                parapluieOuvert.SetActive(false);
                FMODUnity.RuntimeManager.PlayOneShot("event:/player/close");
                chuteFMOD.start();
                flyFMOD.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            }

            else
            {
                FMODUnity.RuntimeManager.PlayOneShot("event:/player/open");
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

        if (Input.GetButtonDown("Fire3"))
        {
            FlapingNumber += FlapNumberCheat;
            EnergieFlap = 100f;
        }
        EnergieFlap += SpeedResetFlap * Time.deltaTime;
        SliderE.value = EnergieFlap;
        if (EnergieFlap >= 100)
        {
            EnergieDown = false;
            SliderBG.color = ColorSliderUp;
            EnergieFlap = 100f;
        }
    }

    void FixedUpdate()
    {
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
    }
    private void OnCollisionStay(Collision other)
    {

    }

    private void OnCollisionExit(Collision other)
    {
        Collision = false;
        //transform.DORotateQuaternion(Quaternion.Euler(0, 0, 0), 1);
    }
}
