using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CamDefDisapear : MonoBehaviour
{
    public TextMeshProUGUI TextCamDef;
    public CanvasGroup TextCamDefDisappear;
    public float speedDisappear;
    
    
    public GameObject camSigne1;
    public GameObject camSigne2;
    public GameObject camSigne3;
    public CameraRotate CameraController;
    void Start()
    {
        TextCamDefDisappear = gameObject.GetComponent<CanvasGroup>();
    }


    void Update()
    {
        if (TextCamDefDisappear.alpha > 0f)
        {
            TextCamDefDisappear.alpha -= speedDisappear * Time.deltaTime;
        }
    }

    public void ChangeCam()
    {
        TextCamDefDisappear.alpha = 1f;
        
        if (CameraController.CameraControl == 0)
        {
            camSigne1.SetActive(true);
            camSigne2.SetActive(false);
            camSigne3.SetActive(false);
            TextCamDef.text = "Camera fixe";
        }
        if (CameraController.CameraControl == 1)
        {
            camSigne1.SetActive(false);
            camSigne2.SetActive(true);
            camSigne3.SetActive(false);
            TextCamDef.text = "Camera libre";
        }
        if (CameraController.CameraControl == 2)
        {
            camSigne1.SetActive(false);
            camSigne2.SetActive(false);
            camSigne3.SetActive(true);
            TextCamDef.text = "Camera focus";
        }
    }
    
}
