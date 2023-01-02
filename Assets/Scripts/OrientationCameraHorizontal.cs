using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrientationCameraHorizontal : MonoBehaviour
{
    public Transform CameraPivot;
    public Transform OrientationIndication;
    void Start()
    {
        
    }

    void Update()
    {
        transform.position = new Vector3(OrientationIndication.position.x,CameraPivot.position.y,OrientationIndication.position.z);
    }
}
