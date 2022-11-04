using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : TemplateState
{
    public override void StartState(StateMachineMob1 Mob1)
    {
        Mob1.Nav.stoppingDistance = 0;
    }
    public override void UpdateState(StateMachineMob1 Mob1)
    {
        //changements de state
        if (Mob1.Cible != null)
        {
            Mob1.ChangeState(Mob1.attackState);
            Mob1.GoTo = false;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            Mob1.GoTo = false;
            Mob1.Cible = null;
            Mob1.Nav.destination = Mob1.Player.position;
            Mob1.ChangeState(Mob1.followState);
            Mob1.GoTo = false;
            Mob1.Cible = null;
            Mob1.CameraM.GetComponent<PlayerColorsControl>().InstantPS();
        }
        //changements de state
    }
}
