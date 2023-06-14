using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class TornadeHeight : MonoBehaviour
{
    public float hauteurMax;
    public GameObject Parapluie; 
    
    void Update()
    {
        gameObject.transform.position = new Vector3 (transform.position.x, Parapluie.transform.position.y, transform.position.z);
        if(transform.position.y >= hauteurMax) gameObject.transform.position = new Vector3(transform.position.x, hauteurMax, transform.position.z);
        transform.LookAt (Parapluie.transform.position);
        //Quaternion target = Quaternion.Euler(transform.localRotation.x, 0, transform.localRotation.z);
        //gameObject.transform.rotation = target;
    }
}
