using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialReplaceForMask : MonoBehaviour
{
    public Material baseMaterial;
    public Material maskMaterial;

    Renderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    public void SwitchToMaskMaterial()
    {
        _renderer.material = maskMaterial;
    }

    public void SwitchToBaseMaterial()
    {
        _renderer.material = baseMaterial;
    }
}
