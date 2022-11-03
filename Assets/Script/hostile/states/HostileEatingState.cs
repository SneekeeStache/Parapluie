using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HostileEatingState : HostileBaseState
{
    float gainNutritionel=50;
   public override void enterState(HostileBehavior hostile)
    {

    }
    public override void updateState(HostileBehavior hostile)
    {
        Debug.Log("joue animation manger");
        hostile.hunger+=gainNutritionel;
        hostile.changeState(hostile.HostilePatroleState);

    }

    public override void onCollisionEnter(HostileBehavior hostile)
    {
        throw new System.NotImplementedException();
    }
}
