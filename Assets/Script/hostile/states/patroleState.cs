using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class patroleState : HostileBaseState
{

    [SerializeField] int listPosition = 0;
    List<Transform> waypoints;
    //code se lançant a l'entrer dans l'etat
    public override void enterState(HostileBehavior hostile)
    {
        hostile.agent.speed=2f;
        hostile.agent.stoppingDistance=0;

        //prend la liste des waypoints assigner dans l'editeur
        waypoints = hostile.waypoints;
        hostile.agent.destination = waypoints[listPosition].position;
    }
    public override void updateState(HostileBehavior hostile)
    {
        //code de patrouille
        float listPositionFloat = (float)listPosition;
        float sizeWaypoint = (float)waypoints.Count;
        if (hostile.agent.remainingDistance <= 0)
        {
            if (listPosition >= waypoints.Count - 1)
            {
                listPosition = 0;
            }
            else
            {
                listPosition++;
            }
            hostile.agent.destination = waypoints[listPosition].position;
        }
        //verifie si une cible est a porter
        foreach (GameObject uneCible in hostile.listCible)
        {
            //Debug.DrawRay(hostile.agentTransform.position, (hostile.agentTransform.position - uneCible.transform.position) * 10, Color.red);
            float DistanceCibleHostile = Vector3.Distance(hostile.agentTransform.position, uneCible.transform.position);
            if (DistanceCibleHostile <= hostile.DistanceDetection)
            {
                RaycastHit hit;
                if (Physics.Raycast(hostile.agentTransform.position, uneCible.transform.position - hostile.agentTransform.position, out hit, hostile.DistanceDetection))
                {
                    if (hit.collider.CompareTag("PlayerMonster"))
                    {
                        float angleDetection = Vector3.Angle(hostile.agentTransform.position, uneCible.transform.position - hostile.agentTransform.position);
                        if (angleDetection <= hostile.maxAngleDetection && angleDetection >= -hostile.maxAngleDetection)
                        {
                            hostile.cible=uneCible;
                            hostile.changeState(hostile.chasingState);
                        }
                    }
                }
            }

        }

    }
    public override void onCollisionEnter(HostileBehavior hostile)
    {
        //si ont veut utilisé du code en fonction des collision;
    }
}
