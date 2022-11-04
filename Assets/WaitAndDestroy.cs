using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitAndDestroy : MonoBehaviour
{
    public float timer;

    void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0f)
        {
            Destroy(gameObject);
        }
    }
}
