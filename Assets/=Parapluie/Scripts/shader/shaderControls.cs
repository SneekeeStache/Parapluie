using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shaderControls : MonoBehaviour
{
    public static int posID = Shader.PropertyToID("_position");
    public static int sizeID = Shader.PropertyToID("_size");
    
    Material wallMaterial;

    public shaderCollider ScriptColliderMur;

    public LayerMask mask;
    // Start is called before the first frame update
    void Start()
    {
        wallMaterial = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        if (ScriptColliderMur.CameraIn)
        {
            wallMaterial.SetFloat(sizeID,5);
        }
        else
        {
            wallMaterial.SetFloat(sizeID,0);
        }

    }
}
