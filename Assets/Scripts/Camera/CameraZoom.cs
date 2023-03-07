using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public GameObject CameraController;
    public player parapluie;
    public float minC, maxC;
    public float Cvalue;

    [Header("Cinematique de fin")]
    public float speedCam;

    public float timeCam;

    public void CamReset()
    {
        CameraController.transform.localRotation = Quaternion.Euler(25, 145, 0);
        transform.localPosition = new Vector3(0,0,-120);
        //Debug.Log("camReset");
    }

    void Update()
    {
        if (parapluie.end)
        {
            if (timeCam >= 0)
            {
                //transform.localPosition += new Vector3(0, 0, speedCam) * Time.deltaTime;
                transform.position -= transform.forward * (Time.deltaTime * speedCam);
                //transform.position -= (transform.forward * Time.deltaTime * speedCam);
                timeCam -= Time.deltaTime;
            }
        }
        /*if (Input.mouseScrollDelta.y > 0f && Cvalue<maxC)
        {
            
            transform.position += transform.forward;
            Cvalue += 0.5f;
        }
        if (Input.mouseScrollDelta.y < 0f && Cvalue>minC)
        {
            transform.position += -transform.forward;
            Cvalue -= 0.5f;
        }*/
    }
}
