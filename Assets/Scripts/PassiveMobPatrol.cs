using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PassiveMobPatrol : MonoBehaviour
{
    public NavMeshAgent selfNavMeshAgentAgent;
    public Vector3 targetPosition;
    public bool idle;
    public float timer;
    public float timeIdle;

    // Start is called before the first frame update
    void Start()
    {
        idle = true;
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (idle)
        {
            timer += Time.deltaTime;

            
        }
        
        if (timer > timeIdle)
        {
            targetPosition = new Vector3(transform.position.x + Random.Range(-20f, 20f), transform.position.y, transform.position.z + Random.Range(-20f, 20f));
            selfNavMeshAgentAgent.destination = targetPosition;
            timer = 0;
            idle = false;
        }

        if (Vector3.Distance(transform.position,targetPosition) <= 0.1f)
        {
            idle = true;
        }
    }
}
