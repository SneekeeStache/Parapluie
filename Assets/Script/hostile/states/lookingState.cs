using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lookingState : HostileBaseState
{
    float timerLooking=0;
    float numberlook=0;
    public override void enterState(HostileBehavior hostile)
    {
        hostile.agent.destination=hostile.cible.transform.position;
    }
    public override void updateState(HostileBehavior hostile)
    {
        if(timerLooking < hostile.durationTimerLooking){
            timerLooking += Time.deltaTime;
        }else{
            hostile.agent.destination= new Vector3(hostile.agentTransform.position.x+Random.Range(-10,10),1,hostile.agentTransform.position.z+Random.Range(-10,10));
            numberlook++;
        }
        
        if(numberlook>= hostile.numberlook){
            hostile.changeState(hostile.patroleState);
        }
    }
    public override void onCollisionEnter(HostileBehavior hostile)
    {
        throw new System.NotImplementedException();
    }
}
