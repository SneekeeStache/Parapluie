using UnityEngine;

public abstract class HostileBaseState
{

    public abstract void enterState(HostileBehavior hostile);
    public abstract void updateState(HostileBehavior hostile);
    public abstract void onCollisionEnter(HostileBehavior hostile);

   

}
