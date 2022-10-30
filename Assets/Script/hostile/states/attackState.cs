using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attackState : HostileBaseState
{
    public override void enterState(HostileBehavior hostile)
    {
        
    }
    public override void updateState(HostileBehavior hostile)
    {
        if(Vector3.Distance(hostile.agentTransform.position,hostile.cible.transform.position) <= hostile.distanceAttaque){
            Debug.Log("inserer animation attaque avec lancement du script degat pendant animation");
        }else{
            hostile.changeState(hostile.chasingState);
        }
    }
    public override void onCollisionEnter(HostileBehavior hostile)
    {
        throw new System.NotImplementedException();
    }
}
