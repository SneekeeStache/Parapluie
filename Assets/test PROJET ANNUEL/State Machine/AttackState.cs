using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : TemplateState
{
    public override void StartState(StateMachineMob1 Mob1)
    {
        Mob1.Nav.destination = Mob1.Cible.transform.position;
        Mob1.Nav.stoppingDistance = 3;
    }

    public override void UpdateState(StateMachineMob1 Mob1)
    {
        Mob1.Nav.destination = Mob1.Cible.transform.position;

        //changements de state
        /*        if (Input.GetKeyDown(KeyCode.T))
                {
                    Mob1.Nav.destination = Mob1.transform.position;
                    Mob1.Cible = null;
                    Mob1.ChangeState(Mob1.idleState);
                }*/
/*        if (Mob1.Cible != null)
        {
            Mob1.ChangeState(Mob1.attackState);
        }*/
        if (Mob1.GoTo)
        {
            Mob1.ChangeState(Mob1.patrolState);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            Mob1.Nav.destination = Mob1.Player.position;
            Mob1.Cible = null;
            Mob1.ChangeState(Mob1.followState);

            Mob1.CameraM.GetComponent<PlayerColorsControl>().InstantPS();
        }
        //changements de state
    }
}
