using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorZone : MonoBehaviour
{
    public bool gizmos;
    public Color color;
    public Color colorParapluie;
    public Color colorEdge;
    public float radius;

    private void OnDrawGizmos()
    {
        // Draw a color sphere at the transform's position
        Gizmos.color = color;
        if(gizmos) Gizmos.DrawSphere(transform.position, radius);
    }
}
