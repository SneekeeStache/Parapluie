using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawningMobs : MonoBehaviour
{
    public GameObject mob1;
    public GameObject mob2;
    public GameObject mob3;

    public float timer;
    public float timeToSpawn;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            Instantiate(mob1, transform.position, transform.rotation);
        }
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            Instantiate(mob2, transform.position, transform.rotation);
        }
        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            Instantiate(mob3, transform.position, transform.rotation);
        }

        float random = Random.Range(0,3);
        Debug.Log(random);

        timer += Time.deltaTime;
        
        if (timer> timeToSpawn)
        {
            timer = 0;
            if (random == 0)
            {
                Instantiate(mob1, transform.position, transform.rotation);
            }

            if (random == 1)
            {
                Instantiate(mob2, transform.position, transform.rotation);
            }

            if (random == 2)
            {
                Instantiate(mob3, transform.position, transform.rotation);
            }
        }
    }
}
