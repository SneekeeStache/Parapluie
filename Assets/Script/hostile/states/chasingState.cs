using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chasingState : HostileBaseState
{
    
    float timer=0;
    float timerLostHostile=0;
    float durationTimerPathFinding=0.2f;
    public override void enterState(HostileBehavior hostile)
    {
        hostile.agent.speed=3f;
        hostile.agent.stoppingDistance=2;
    }
    public override void updateState(HostileBehavior hostile)
    {
        Debug.Log(timerLostHostile);
        hostile.agentTransform.rotation=Quaternion.LookRotation(hostile.cible.transform.position - hostile.agentTransform.position,Vector3.up);
        RaycastHit hit;
        if(Physics.Raycast(hostile.agentTransform.position,hostile.cible.transform.position - hostile.agentTransform.position,out hit,hostile.DistanceDetection)){
            if(hit.collider.tag==hostile.cible.tag){
                timerLostHostile=0;
                if(timer < durationTimerPathFinding){
                    timer+= Time.deltaTime;
                }else{
                    hostile.agent.destination=hostile.cible.transform.position;
                    timer=0;
                }
            }else{
                if(timerLostHostile < hostile.durationTimerLostHostile){
                    timerLostHostile += Time.deltaTime;
                }else{
                    Debug.Log("changement looking");
                    hostile.agent.destination=hostile.cible.transform.position;
                    hostile.changeState(hostile.lookingState);
                }
            }
        }else{
                if(timerLostHostile < hostile.durationTimerLostHostile){
                    timerLostHostile += Time.deltaTime;
                }else{
                    Debug.Log("changement looking");
                    hostile.changeState(hostile.lookingState);
                }
            }
        
        if(Vector3.Distance(hostile.agentTransform.position,hostile.cible.transform.position)<= hostile.distanceAttaque){
            hostile.changeState(hostile.attackState);
        }
    }
    public override void onCollisionEnter(HostileBehavior hostile)
    {
        throw new System.NotImplementedException();
    }
}
