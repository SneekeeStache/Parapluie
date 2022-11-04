using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HostileLookingState : HostileBaseState
{
    float timerLooking = 0;
    float numberlook = 0;
    public override void enterState(HostileBehavior hostile)
    {
        numberlook = 0;
        hostile.agent.destination = hostile.cible.transform.position;
    }
    public override void updateState(HostileBehavior hostile)
    //va dans une direction aleatoire pour chercher la cible
    {
        if (hostile.health <= 0)
        {
            hostile.changeState(hostile.HostileDeadState);
        }
        if (timerLooking < hostile.durationTimerLooking)
        {
            timerLooking += Time.deltaTime;
        }
        else
        {
            hostile.agent.destination = new Vector3(hostile.agentTransform.position.x + Random.Range(-10, 10), 1, hostile.agentTransform.position.z + Random.Range(-10, 10));
            numberlook++;
        }

        if (numberlook >= hostile.numberlook)
        {
            hostile.changeState(hostile.HostilePatroleState);
        }
        //si trouve la cible, repasse en etat chasse
        RaycastHit hit;
        if (Physics.Raycast(hostile.agentTransform.position, hostile.cible.transform.position - hostile.agentTransform.position, out hit, hostile.DistanceDetection))
        {
            if (hit.collider.CompareTag("PlayerMonster"))
            {

                float angleDetection = Vector3.Angle(hostile.agentTransform.forward, hostile.cible.transform.position - hostile.agentTransform.position);
                if (angleDetection <= hostile.maxAngleDetection && angleDetection >= -hostile.maxAngleDetection)
                {
                    hostile.changeState(hostile.HostileChasingState);
                }
            }
        }
    }
    public override void onCollisionEnter(HostileBehavior hostile)
    {
        throw new System.NotImplementedException();
    }
}
