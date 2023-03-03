using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFocus : MonoBehaviour
{
    public Transform CibleCameraTransform;
    private GameObject CameraController;
    void Start()
    {
        CameraController = GameObject.Find("CameraController");
    }
    
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject == CameraController)
        {
            collision.gameObject.GetComponent<CameraRotate>().FocusTransform = CibleCameraTransform;
            collision.gameObject.GetComponent<CameraRotate>().CameraControl = 2;
        }
    }
}
