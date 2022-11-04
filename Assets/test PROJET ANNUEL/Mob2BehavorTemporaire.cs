using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mob2BehavorTemporaire : MonoBehaviour
{
    private NavMeshAgent Nav;
    public float timer;
    private float timerR;

    void Start()
    {
        Nav = gameObject.GetComponent<NavMeshAgent>();
        timerR = timer;
    }

    
    void Update()
    {
        timerR -= Time.deltaTime;
        if(timerR <= 0f)
        {
            timerR = timer;
            Nav.destination = new Vector3(Random.Range(-20,20),0, Random.Range(-20, 20));
        }
    }
}
