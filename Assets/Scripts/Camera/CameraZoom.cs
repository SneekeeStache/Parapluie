using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public GameObject CameraController;
    public float minC, maxC;
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
    
    void Update()
    {
        distanceCenterCamera=Mathf.Clamp(distanceCenterCamera,minC,maxC);

        if (Input.mouseScrollDelta.y > 0f)
        {
            
            distanceCenterCamera+=1*vitesseZoom*Time.deltaTime;
        }
        if (Input.mouseScrollDelta.y < 0f)
        {
            distanceCenterCamera-=1*vitesseZoom*Time.deltaTime;
        }
        Vector3 desiredCameraPos= transform.parent.TransformPoint(dollyDir * distanceCenterCamera);
        RaycastHit hit;
        if(Physics.Linecast(transform.parent.position,desiredCameraPos,out hit,ignoreLayer)){
            print(hit.collider.gameObject.name);
            distance=Mathf.Clamp((hit.distance * 0.87f),minC,distanceCenterCamera);

        }else{
            distance=distanceCenterCamera;
        }

        transform.localPosition = Vector3.Lerp (transform.localPosition, dollyDir * distance, Time.deltaTime * smooth);

        Debug.DrawLine(transform.parent.position,desiredCameraPos,Color.red);
    }
}
