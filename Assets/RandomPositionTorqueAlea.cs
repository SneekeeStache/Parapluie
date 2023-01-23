using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPositionTorqueAlea : MonoBehaviour
{
    public Transform ParapluieOrientation;
    public float randomSize;
    public float Hauteur;
    public float timerReset;
    private float timer;

    void Start()
    {
        timer = timerReset;
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            transform.position = new Vector3(ParapluieOrientation.position.x + Random.Range(-randomSize,randomSize),ParapluieOrientation.position.y + Hauteur, ParapluieOrientation.position.z + Random.Range(-randomSize,randomSize));
            timer = timerReset;
        }
        transform.position = new Vector3(transform.position.x,ParapluieOrientation.position.y + Hauteur,transform.position.z);
    }
}
