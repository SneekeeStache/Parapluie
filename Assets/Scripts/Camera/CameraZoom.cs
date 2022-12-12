using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public GameObject CameraController;
    public float minC, maxC;
    public float Cvalue;
    
    void Update()
    {
        if (Input.mouseScrollDelta.y > 0f && Cvalue<maxC)
        {
            
            transform.position += transform.forward;
            Cvalue += 0.5f;
        }
        if (Input.mouseScrollDelta.y < 0f && Cvalue>minC)
        {
            transform.position += -transform.forward;
            Cvalue -= 0.5f;
        }
    }
}
