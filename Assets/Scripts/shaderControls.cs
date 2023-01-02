using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shaderControls : MonoBehaviour
{
    public static int posID = Shader.PropertyToID("_position");
    public static int sizeID = Shader.PropertyToID("_size");
    
    Material wallMaterial;

    public Camera mainCamera;
    public Transform player;

    public LayerMask mask;
    // Start is called before the first frame update
    void Start()
    {
        wallMaterial = GetComponent<Renderer>().material;

    }

    // Update is called once per frame
    void Update()
    {
        var dir =  mainCamera.transform.position - transform.position;
        var ray = new Ray(transform.position, dir.normalized);
        RaycastHit hit;
        Debug.DrawRay(transform.position, dir.normalized * 30, Color.red);

        if (Physics.Raycast(transform.position,dir.normalized,out hit,30))
        {
            if (hit.collider.CompareTag("MainCamera"))
            {
                wallMaterial.SetFloat(sizeID,1);
                print("test");
            }
            else
            {
                wallMaterial.SetFloat(sizeID,0);
            }
            
        }
        else
        {
            wallMaterial.SetFloat(sizeID,0);
        }

        var view = mainCamera.WorldToViewportPoint(transform.position);
    }
}
