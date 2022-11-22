using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public GameObject CameraController;
    public float minC, maxC;
    void Start()
    {

    }

    
    void Update()
    {
        /*if (Input.mouseScrollDelta.y > 0f && transform.position.z > minC)
        {
            transform.position = new Vector3(0,0,transform.position.z + 5f);
        }
        if (Input.mouseScrollDelta.y < 0f && transform.position.z < maxC)
        {
            transform.position = new Vector3(0,0,transform.position.z - 5f);
        }*/
        Debug.Log(Vector3.Distance(CameraController.transform.position, transform.position));
        if (Input.mouseScrollDelta.y > 0f && Vector3.Distance(CameraController.transform.position, transform.position) < maxC)
        {
            
            transform.position += transform.forward;
        }
        if (Input.mouseScrollDelta.y < 0f && Vector3.Distance(CameraController.transform.position, transform.position) > minC)
        {
            transform.position += -transform.forward;
        }
    }
}
