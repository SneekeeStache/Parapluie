using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TornadeHeight : MonoBehaviour
{
    public float hauteurMax;
    private GameObject player; 

    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    
    void Update()
    {
        gameObject.transform.position = new Vector3 (transform.position.x, player.transform.position.y, transform.position.z);
        if(transform.position.y >= hauteurMax) gameObject.transform.position = new Vector3(transform.position.x, hauteurMax, transform.position.z);
        transform.LookAt (player.transform.position);
        //Quaternion target = Quaternion.Euler(transform.localRotation.x, 0, transform.localRotation.z);
        //gameObject.transform.rotation = target;
    }
}
