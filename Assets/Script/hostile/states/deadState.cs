using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deadState : HostileBaseState
{
    public override void enterState(HostileBehavior hostile)
    {
        Debug.Log("i'm dead");
    }
    public override void updateState(HostileBehavior hostile)
    {

    }
    public override void onCollisionEnter(HostileBehavior hostile)
    {

    }
}
