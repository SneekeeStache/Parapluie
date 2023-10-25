using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    public Transform Cible;
    public Transform SelfTransform;
    void Update()
    {
        SelfTransform.LookAt(Cible);
    }
}
