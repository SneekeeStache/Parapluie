using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tyrolienne : MonoBehaviour
{
    public Transform depart;
    public Vector3 fin;
    public GameObject parapluie;
    private bool canTyrolienne;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && canTyrolienne)
        {
            parapluie.transform.Translate(fin);
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if( other.CompareTag("Player")){
            canTyrolienne = true;
        }
    }
    private void OnTriggerExit(Collider other) {
        if( other.CompareTag("Player")){
            canTyrolienne = false;
        }
    }
}
