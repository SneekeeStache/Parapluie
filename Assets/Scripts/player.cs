using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    [Header("Variables changeants les controles")]
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


    void Start()
    {
        transform.DORotateQuaternion(Quaternion.Euler(0, 0, 0), 1);
        rb = GetComponent<Rigidbody>();
        animatorPlayer = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            fermer = true;
            rb.AddForce((OrietationJump.transform.position - transform.position) * ForceJump, ForceMode.Impulse);
            Cone.SetActive(false);
        }
        else if (Input.GetButtonUp("Jump"))
        {
            fermer = false;
            Cone.SetActive(true);
        }
    }

    void FixedUpdate()
    {
        orientationModif = OrientationVent + new Vector3(Input.GetAxis("Horizontal") * ImpulseOrientationPlayer, 0, Input.GetAxis("Vertical") * ImpulseOrientationPlayer);
        orientationAnim = OrientationVent + new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));


        if (fermer)
        {
            rb.drag = dragFermer;
        }
        else
        {
            rb.drag = drag;
            rb.AddForce(orientationModif, ForceMode.Impulse);
        }


        //Debug.Log(Collision);
        if (Collision)
        {
            rb.freezeRotation = false;
        }
        else
        {
            rb.freezeRotation = true;
            if (Input.GetAxisRaw("Horizontal") > 0f || Input.GetAxisRaw("Horizontal") < 0f || Input.GetAxisRaw("Vertical") > 0f || Input.GetAxisRaw("Vertical") < 0f)
            {
                transform.DORotateQuaternion(Quaternion.Euler(orientationAnim.z * forceOrientationAnimation, 0, -orientationAnim.x * forceOrientationAnimation), TimerOrientation);
            }
            transform.DORotateQuaternion(Quaternion.Euler(orientationAnim.z * forceOrientationAnimation, 0, -orientationAnim.x * forceOrientationAnimation), TimerStabilisation);
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
