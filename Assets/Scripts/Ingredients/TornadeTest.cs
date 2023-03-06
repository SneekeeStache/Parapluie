using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TornadeTest : MonoBehaviour
{
    private Vent ventScript;
    public bool x, y, z;
    public float limX, limY, limZ;
    private float xT, yT, zT;
    public float speed;

    private void Start()
    {
        ventScript = gameObject.GetComponent<Vent>();
    }

    void Update()
    {
        if (x)
        {
            xT = (Mathf.Repeat(Time.time * speed, limX*2)) - (limX);
            ventScript.ajoutVent.x = xT;
        }
        if (y)
        {
            yT = (Mathf.Repeat(Time.time * speed, limY * 2)) - (limY);
            ventScript.ajoutVent.y = yT;
        }
        if (z)
        {
            zT = (Mathf.Repeat(Time.time * speed, limZ * 2)) - (limZ);
            ventScript.ajoutVent.z = zT;
        }
    }
}
