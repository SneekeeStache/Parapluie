using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class player : MonoBehaviour
{
    [Header("Temporaire")]
    public GameObject Cone;

    [Header("Composants à récupérer")]
    public GameObject OrietationJump;
    bool Collision = false;
    public bool fermer = false;
    Rigidbody rb;
    Animator animatorPlayer;

    private Transform cameraTransform;
    public Text NombreFlapText;

    [Header("Variables changeants les controles")]

    [SerializeField] public float NombreFlap;

    public float FlapingNumber;
    [SerializeField] float ForceJump;
    [SerializeField] float TimerOrientation;
    [SerializeField] float TimerStabilisation;
    [SerializeField] float drag = 7;
    [SerializeField] float dragFermer = 0;
    [SerializeField] float ImpulseOrientationPlayer;
    [SerializeField] float forceOrientationAnimation;
    [SerializeField] Vector3 OrientationVent = Vector3.zero;


    [SerializeField] Vector3 orientationModif;
    [SerializeField] Vector3 orientationAnim;

    private float timer = 0.3f;
    private float timerReset = 0.3f;
    public bool ActiveTimer;


    void Start()
    {
        transform.DORotateQuaternion(Quaternion.Euler(0, 0, 0), 1);
        rb = GetComponent<Rigidbody>();
        animatorPlayer = GetComponent<Animator>();
        FlapingNumber = NombreFlap;
        NombreFlapText.text=FlapingNumber.ToString();
        cameraTransform = GameObject.Find("Main Camera").transform;
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump") && FlapingNumber >= 1f && !fermer)
        {
            rb.AddForce((OrietationJump.transform.position - transform.position) * ForceJump, ForceMode.Impulse);
            Cone.SetActive(false);
            FlapingNumber  = FlapingNumber - 1f;
            ActiveTimer = true;
        }
        if (Input.GetButtonDown("Fire1") && ActiveTimer == false)
        {
            fermer = !fermer;
            if (fermer)Cone.SetActive(false);
            else Cone.SetActive(true);
        }
        if (ActiveTimer) timer -= Time.deltaTime;
        if (timer<=0f){
            Cone.SetActive(true);
            timer = timerReset;
            ActiveTimer = false;
        }
        NombreFlapText.text=FlapingNumber.ToString();
    }

    void FixedUpdate()
    {
        orientationModif = OrientationVent + ((cameraTransform.right* Input.GetAxis("Horizontal") + cameraTransform.forward *  Input.GetAxis("Vertical")) * ImpulseOrientationPlayer);
        orientationAnim = OrientationVent + cameraTransform.right *Input.GetAxis("Horizontal")+ cameraTransform.forward*Input.GetAxis("Vertical");


        if (fermer)
        {
            rb.drag = dragFermer;
        }
        else
        {
            rb.drag = drag;
            rb.AddForce(orientationModif, ForceMode.Impulse);
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
                    rb.AddTorque(-cameraTransform.forward*forceOrientationAnimation,ForceMode.Acceleration);
                }else if (Input.GetAxisRaw("Horizontal") < 0f)
                {
                    rb.AddTorque(cameraTransform.forward*forceOrientationAnimation,ForceMode.Acceleration);
                }
                if(Input.GetAxisRaw("Vertical") > 0f)
                {
                    rb.AddTorque(cameraTransform.right*forceOrientationAnimation,ForceMode.Acceleration);
                } else if(Input.GetAxisRaw("Vertical") < 0f)
                {
                    rb.AddTorque(-cameraTransform.right*forceOrientationAnimation,ForceMode.Acceleration);
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
        transform.DORotateQuaternion(Quaternion.Euler(0, 0, 0), 1);
    }
}
