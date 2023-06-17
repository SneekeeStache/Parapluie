using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ROTATION : MonoBehaviour
{
    public float speed;
    public bool X;
    public bool Y;
    public bool Z;
    
    void Update()
    {
        if(X) transform.Rotate(speed * Time.deltaTime, 0.0f, 0.0f, Space.World);
        if(Y) transform.Rotate(0.0f, speed * Time.deltaTime, 0.0f, Space.World);
        if(Z) transform.Rotate(0.0f, 0.0f, speed * Time.deltaTime, Space.World);
    }
}
