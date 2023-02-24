using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCamColor : MonoBehaviour
{
    public Color color;
    public Material myColor;
    private Camera cameraMain;
    private float t = 0f;
    public float speedColorChange;

    void Start()
    {
        cameraMain = Camera.main;
    }

    public void OnTriggerEnter(Collider other)
    {
        {
            if (other.gameObject.CompareTag("Player"))
            {
                cameraMain.gameObject.GetComponent<PencilContour>().backgroundColor = Color.Lerp(cameraMain.gameObject.GetComponent<PencilContour>().backgroundColor, color, 1f);
                myColor.color = Color.Lerp(cameraMain.gameObject.GetComponent<PencilContour>().backgroundColor, color, 1f);
                t += Time.deltaTime * speedColorChange;
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            t = 0;
        }
    }
}
