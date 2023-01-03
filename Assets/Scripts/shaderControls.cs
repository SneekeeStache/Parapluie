using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shaderControls : MonoBehaviour
{
    public static int posID = Shader.PropertyToID("_position");
    public static int sizeID = Shader.PropertyToID("_size");

    [Range(0, 10)] [SerializeField] private float taille;
    
    Material wallMaterial;


    public shaderCollider ScriptColliderMur;
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
            wallMaterial.SetFloat(sizeID,taille);
        }
        else
        {
            wallMaterial.SetFloat(sizeID,0);
        }

    }
}
