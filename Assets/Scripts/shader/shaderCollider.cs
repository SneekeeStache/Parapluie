using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shaderCollider : MonoBehaviour
{
    public bool CameraIn = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            CameraIn = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        CameraIn = false;
    }
}
