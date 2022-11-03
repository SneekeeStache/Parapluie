using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HostileAttackState : HostileBaseState
{
    float timerAttaque=0;
    float cibleHealth;
    public override void enterState(HostileBehavior hostile)
    {
        cibleHealth=hostile.cible.GetComponent<testDummy>().health;
    }
    public override void updateState(HostileBehavior hostile)
    {
        if (hostile.health <= 0)
        {
            hostile.changeState(hostile.HostileDeadState);
        }
        RaycastHit hit;
        if (Physics.Raycast(hostile.agentTransform.position, hostile.cible.transform.position - hostile.agentTransform.position, out hit, hostile.distanceAttaque)){
            if(timerAttaque < 3){
                timerAttaque+=Time.deltaTime;
            }else{
                Debug.Log("ajouter animation attaque avec code attaque dans l'animation");
                cibleHealth -= hostile.damage;
                timerAttaque=0;
            }
            if(cibleHealth <=0){
                hostile.changeState(hostile.HostileEatingState);
            }
        }
        else
        {
            hostile.changeState(hostile.HostileChasingState);
        }
    }
    public override void onCollisionEnter(HostileBehavior hostile)
    {
        throw new System.NotImplementedException();
    }
}
