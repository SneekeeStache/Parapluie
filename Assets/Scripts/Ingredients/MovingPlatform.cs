using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] GameObject[] Waypoints;
    private int CurrentWaypointIndex = 0;
    public int StartPoint = 0;
    public int NombreTour;
    public bool ToursIlimite;
    public bool Boucle;
    public bool InvertDirection;
    [SerializeField] float speed;
    void Start()
    {
        CurrentWaypointIndex = StartPoint;
        transform.position = Waypoints[CurrentWaypointIndex].transform.position;
        if (ToursIlimite) NombreTour = 100;
    }


    void Update()
    {
        if (ToursIlimite) NombreTour = 100;

        if (Vector3.Distance(transform.position, Waypoints[CurrentWaypointIndex].transform.position) < 0.1f && NombreTour > 0)
        {
            if (Boucle)
            {
                if (!InvertDirection)
                {
                    CurrentWaypointIndex++;
                    if (CurrentWaypointIndex >= Waypoints.Length)
                    {
                        CurrentWaypointIndex = 0;
                        NombreTour--;
                    }

                }
                else
                {
                    CurrentWaypointIndex--;
                    if (CurrentWaypointIndex < 0)
                    {
                        CurrentWaypointIndex = Waypoints.Length - 1;
                        NombreTour--;
                    }
                }
            }
            if (!Boucle && NombreTour > 0)
            {
                if (CurrentWaypointIndex >= Waypoints.Length - 1)
                {
                    InvertDirection = !InvertDirection;
                    NombreTour--;
                }
                if (CurrentWaypointIndex <= 0)
                {
                    InvertDirection = !InvertDirection;
                    NombreTour--;

                }
                if (InvertDirection) CurrentWaypointIndex++;
                if (!InvertDirection) CurrentWaypointIndex--;
            }
            //CurrentWaypointIndex++;
            //if (CurrentWaypointIndex >= Waypoints.Length) CurrentWaypointIndex = 0;

        }
        transform.position = Vector3.MoveTowards(transform.position, Waypoints[CurrentWaypointIndex].transform.position, speed * Time.deltaTime);
    }
}
