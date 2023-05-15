using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMaskHelper : MonoBehaviour
{
    public Camera maskCamera;
    void LateUpdate()
    {
        MaterialReplaceForMask[] materials = FindObjectsOfType<MaterialReplaceForMask>();

        for (int i = 0; i < materials.Length; i++)
        {
            materials[i].SwitchToMaskMaterial();
        }

        maskCamera.Render();

        for (int i = 0; i < materials.Length; i++)
        {
            materials[i].SwitchToBaseMaterial();
        }

    }
}
