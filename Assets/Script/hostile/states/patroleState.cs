using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class patroleState : HostileBaseState
{
    
    [SerializeField] int listPosition=0;
    List<Transform> waypoints;
    float distanceCibleProche;
    public override void enterState(HostileBehavior hostile)
    {
        distanceCibleProche=Mathf.Infinity;
        waypoints=hostile.waypoints;
        hostile.agent.destination=waypoints[listPosition].position;
    }
    public override void updateState(HostileBehavior hostile)
    {
        float listPositionFloat=(float)listPosition;
        float sizeWaypoint = (float)waypoints.Count;
        if(hostile.agent.remainingDistance <=0){
            if(listPosition >= waypoints.Count -1)
            {
                listPosition=0;
            }else{
                listPosition++;
            }
            hostile.agent.destination=waypoints[listPosition].position;
        }

        foreach(GameObject uneCible in  hostile.listCible){
            float DistanceCibleHostile = Vector3.Distance(hostile.agentTransform.position,uneCible.transform.position);
            if(distanceCibleProche > DistanceCibleHostile){
                distanceCibleProche = DistanceCibleHostile;
                cible=uneCible;
            }

        }
        RaycastHit hit;
        if(Physics.Raycast(hostile.agentTransform.position,cible.transform.position-hostile.agentTransform.position,out hit,hostile.DistanceDetection)){
            if(hit.collider.CompareTag("PlayerMonster")){
                float angleDetection= Vector3.Angle(hostile.agentTransform.position,cible.transform.position - hostile.agentTransform.position) ;
                if( angleDetection <= hostile.maxAngleDetection && angleDetection >= -hostile.maxAngleDetection){
                    hostile.changeState(hostile.chasingState);
                }
            }
        }
        Debug.DrawRay(hostile.agentTransform.position,(hostile.agentTransform.position - cible.transform.position )*10,Color.red);
    }
    public override void onCollisionEnter(HostileBehavior hostile)
    {
        //si ont veut utilisé du code en fonction des collision;
    }
}
