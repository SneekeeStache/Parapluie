using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followParapluieXZ : MonoBehaviour
{
    public Transform parapluie;
    void Update()
    {
        transform.position = new Vector3(parapluie.position.x, gameObject.transform.position.y,parapluie.position.z);
    }
}
