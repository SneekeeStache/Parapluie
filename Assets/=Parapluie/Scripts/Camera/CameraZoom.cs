using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public GameObject CameraController;
    public Player parapluie;
    public float minC, maxC, minMaxC, maxMaxC;
    public float Cvalue;

    public float smooth=10;
    Vector3 dollyDir;
    public Vector3 dollyDirAdjusted;
    public float distance;
    public LayerMask ignoreLayer;

    public float distanceCenterCamera=50;
    public float vitesseZoom;

    private void Awake() {
        dollyDir = transform.transform.localPosition.normalized;
        distance=transform.localPosition.magnitude;
    }
    

    [Header("Cinematique de fin")]
    public float speedCam;

    public float timeCam;

    public void CamReset()
    {
        CameraController.transform.localRotation = Quaternion.Euler(25, 145, 0);
        transform.localPosition = new Vector3(0,0,-200);
        //Debug.Log("camReset");
    }

    void Update()
    {
        distanceCenterCamera=Mathf.Clamp(distanceCenterCamera,minC,maxC);

        if (Input.mouseScrollDelta.y < 0f)
        {
            if (maxC < maxMaxC)
            {
                maxC += 1 * vitesseZoom * Time.deltaTime;
            }
            
        }
        if (Input.mouseScrollDelta.y > 0f)
        {
            if(maxC > minMaxC)
            {
                maxC -= 1 * vitesseZoom * Time.deltaTime;

            }
            //maxC -= 1 * vitesseZoom * Time.deltaTime;

        }

        Vector3 desiredCameraPos= transform.parent.TransformPoint(dollyDir * distanceCenterCamera);
        RaycastHit hit;
        if(Physics.Linecast(transform.parent.position,desiredCameraPos,out hit)){
        if(!hit.collider.CompareTag("Player"))
        {
            if (!hit.collider.CompareTag("direction"))
            {
                if (!hit.collider.CompareTag("vent"))
                {
                        if (!hit.collider.CompareTag("cameraIgnore"))
                        {
                            //print(hit.collider.gameObject.name);
                            distance = Mathf.Clamp((hit.distance * 0.87f), minC, distanceCenterCamera);
                        }
                }
                
            }
            else
            {
                distance=maxC;
            }
            
        }
        else
        {
            distance=maxC;
        }
        }else{
            distance=maxC;
        }

        transform.localPosition = Vector3.Lerp (transform.localPosition, dollyDir * distance, Time.deltaTime * smooth);

        Debug.DrawLine(transform.parent.position,desiredCameraPos,Color.red);
        
        if (parapluie.DisableMove)
        {
            if (timeCam >= 0)
            {
                //transform.localPosition += new Vector3(0, 0, speedCam) * Time.deltaTime;
                transform.position -= transform.forward * (Time.deltaTime * speedCam);
                //transform.position -= (transform.forward * Time.deltaTime * speedCam);
                timeCam -= Time.deltaTime;
            }
        }
    }
    
}
