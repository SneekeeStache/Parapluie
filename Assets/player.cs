using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class player : MonoBehaviour
{
    bool Collision=false;
    Rigidbody rb;
    Animator animatorPlayer;
    // Start is called before the first frame update
    void Start()
    {
        transform.DORotateQuaternion(Quaternion.Euler(-90,0,285),1);
        rb=GetComponent<Rigidbody>();
        animatorPlayer=GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.AddForce(Input.GetAxis("Horizontal"),0,Input.GetAxis("Vertical"),ForceMode.Impulse);
        Debug.Log(Collision);
        if(Collision){
            rb.freezeRotation=false;
        }else{
            rb.freezeRotation=true;
        }
        
    }
    private void OnCollisionEnter(Collision other){
            Collision=true;
    }
    private void OnCollisionStay(Collision other) {
        
    }

    private void OnCollisionExit(Collision other) {
        Collision=false;
        transform.DORotateQuaternion(Quaternion.Euler(-90,0,285),1);
    }
}
