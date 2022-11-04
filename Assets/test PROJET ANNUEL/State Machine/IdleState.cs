using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : TemplateState
{
    private float timer = 5;
    private float timerR = 5;
    private float RangePlayer = 5;
    public override void StartState(StateMachineMob1 Mob1)
    {
        Mob1.Nav.stoppingDistance = 0;
        Mob1.Nav.destination = Mob1.transform.position;
    }
    public override void UpdateState(StateMachineMob1 Mob1)
    {
        timer -= Time.deltaTime;
        if(timer <= 0f)
        {
            timer = timerR;
            Mob1.Nav.destination = Mob1.Player.transform.position + new Vector3(Random.Range(-RangePlayer, RangePlayer), 0, Random.Range(-RangePlayer, RangePlayer));
        }





        //changements de state
        float dist = Vector3.Distance(Mob1.Player.position, Mob1.transform.position);
        //Debug.Log("Distance to other: " + dist);
        if (dist >= 6f)
        {
            Mob1.Nav.destination = Mob1.transform.position;
            Mob1.Cible = null;
            Mob1.ChangeState(Mob1.followState);
        }
        if (Mob1.GoTo)
        {
            Mob1.ChangeState(Mob1.patrolState);
        }
        if (Mob1.Cible != null)
        {
            Mob1.ChangeState(Mob1.attackState);
        }
/*        if (Input.GetKeyDown(KeyCode.A))
        {
            Mob1.Nav.destination = Mob1.Player.position;
            Mob1.Cible = null;
            Mob1.ChangeState(Mob1.followState);
        }*/
        //changements de state
    }
}
