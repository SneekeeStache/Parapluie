using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReferenceRotation : MonoBehaviour
{
    public CameraRotate cameraRotationScript;
    public Vector3 positionRelative;
    void Update()
    {
        positionRelative = new Vector3(0,0,cameraRotationScript.JumpOrientation.transform.rotation.z);
        transform.Rotate(positionRelative);
    }
}
