using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorZone : MonoBehaviour
{
    public Color color;
    public float radius;

    private void OnDrawGizmos()
    {
        // Draw a color sphere at the transform's position
        Gizmos.color = color;
        Gizmos.DrawSphere(transform.position, radius);
    }
}
