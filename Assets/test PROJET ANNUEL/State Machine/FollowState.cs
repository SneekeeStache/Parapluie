using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowState : TemplateState
{
    public override void StartState(StateMachineMob1 Mob1)
    {
        //Mob1.Nav.destination = Mob1.Player.position;
        Mob1.Nav.stoppingDistance = 3;
        Mob1.Nav.destination = Mob1.Player.position;
    }

    public override void UpdateState(StateMachineMob1 Mob1)
    {
        Mob1.Nav.destination = Mob1.Player.position;


        //changements de state
        float dist = Vector3.Distance(Mob1.Player.position, Mob1.transform.position);
        //Debug.Log("Distance to other: " + dist);
        if (dist <= 3f)
        {
            Mob1.Nav.destination = Mob1.transform.position;
            Mob1.Cible = null;
            Mob1.ChangeState(Mob1.idleState);
        }
        /*        if (Input.GetKeyDown(KeyCode.T))
                {
                    Mob1.Nav.destination = Mob1.transform.position;
                    Mob1.Cible = null;
                    Mob1.ChangeState(Mob1.idleState);
                }*/
        if (Mob1.GoTo)
        {
            Mob1.ChangeState(Mob1.patrolState);
        }
        if (Mob1.Cible != null)
        {
            Mob1.ChangeState(Mob1.attackState);
        }
        //changements de state
    }
}
