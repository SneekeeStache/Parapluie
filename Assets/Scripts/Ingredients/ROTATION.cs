using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ROTATION : MonoBehaviour
{
    public float speed;
    void Update()
    {
        transform.Rotate(0.0f, speed * Time.deltaTime, 0.0f, Space.World);
    }
}
