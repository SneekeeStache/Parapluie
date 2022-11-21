using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class player : MonoBehaviour
{
    bool Collision=false;
    bool fermer=false;
    Rigidbody rb;
    Animator animatorPlayer;
    [SerializeField] float drag=7;
    [SerializeField] float dragFermer=0;
    [SerializeField] float ImpulseOrientationPlayer;
    [SerializeField] float forceOrientationAnimation;
    [SerializeField] Vector3 OrientationVent=Vector3.zero;


    Vector3 orientationModif;
    Vector3 orientationAnim;
    // Start is called before the first frame update
    void Start()
    {
        transform.DORotateQuaternion(Quaternion.Euler(0,0,0),1);
        rb=GetComponent<Rigidbody>();
        animatorPlayer=GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        orientationModif=OrientationVent+ new Vector3(Input.GetAxis("Horizontal")*ImpulseOrientationPlayer,0,Input.GetAxis("Vertical")*ImpulseOrientationPlayer);
        orientationAnim=OrientationVent+ new Vector3(Input.GetAxis("Horizontal"),0,Input.GetAxis("Vertical"));
        if(Input.GetButtonDown("Jump")){
            fermer=true;
        }else{
            fermer=false;
        }

        if(fermer){
            rb.drag=dragFermer;
        }else{
            rb.drag=drag;
        }

        rb.AddForce(orientationModif,ForceMode.Impulse);
        
        Debug.Log(Collision);
        if(Collision){
            rb.freezeRotation=false;
        }else{
            rb.freezeRotation=true;
            transform.DORotateQuaternion(Quaternion.Euler(orientationAnim.z*forceOrientationAnimation,0,-orientationAnim.x*forceOrientationAnimation),1);
        }
        
    }
    private void OnCollisionEnter(Collision other){
            Collision=true;
    }
    private void OnCollisionStay(Collision other) {
        
    }

    private void OnCollisionExit(Collision other) {
        Collision=false;
        transform.DORotateQuaternion(Quaternion.Euler(0,0,0),1);
    }
}
