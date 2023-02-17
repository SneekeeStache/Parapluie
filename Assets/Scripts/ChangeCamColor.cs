using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCamColor : MonoBehaviour
{
    public Color color;
    private Camera cameraMain;

    void Start()
    {
        cameraMain = Camera.main;
    }

    public void OnTriggerEnter(Collider other)
    {
        {
            if (other.gameObject.CompareTag("Player"))
            {
                cameraMain.gameObject.GetComponent<PencilContour>().backgroundColor = color;
            }
        }
    }
}
