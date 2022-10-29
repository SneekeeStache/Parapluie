using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chasingState : HostileBaseState
{
    public override void enterState(HostileBehavior hostile)
    {
        Debug.Log("Hello");
    }
    public override void updateState(HostileBehavior hostile)
    {
        throw new System.NotImplementedException();
    }
    public override void onCollisionEnter(HostileBehavior hostile)
    {
        throw new System.NotImplementedException();
    }
}
