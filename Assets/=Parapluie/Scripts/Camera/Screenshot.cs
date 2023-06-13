using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screenshot : MonoBehaviour
{
    int currentFileNumber=1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F12))
        {
                while (System.IO.File.Exists("Assets/Screenshots/screenshot"+currentFileNumber+".png"))
                {
                    currentFileNumber++;
                }

                    ScreenCapture.CaptureScreenshot("Assets/Screenshots/screenshot" + currentFileNumber + ".png");

            }
            
        }
}
