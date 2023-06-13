using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFocus : MonoBehaviour
{
    public Transform CibleCameraTransform;
    public GameObject CameraController;
    private GameObject player;
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        //CameraController = GameObject.Find("CameraController");
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == CameraController || other.gameObject == player)
        {
            CameraController.GetComponent<CameraRotate>().canFocus = false;
            //collision.gameObject.GetComponent<CameraRotate>().FocusTransform = CibleCameraTransform;
            CameraController.gameObject.GetComponent<CameraRotate>().CameraControl = 1;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == CameraController || other.gameObject == player)
        {
            Debug.Log("camFocus");
            CameraController.GetComponent<CameraRotate>().canFocus = true;
            CameraController.gameObject.GetComponent<CameraRotate>().FocusTransform = CibleCameraTransform;
            CameraController.gameObject.GetComponent<CameraRotate>().CameraControl = 2;
        }
    }
    
}
