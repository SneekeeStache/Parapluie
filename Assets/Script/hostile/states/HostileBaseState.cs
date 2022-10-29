using UnityEngine;

public abstract class HostileBaseState
{
    public GameObject cible;
    public abstract void enterState(HostileBehavior hostile);
    public abstract void updateState(HostileBehavior hostile);
    public abstract void onCollisionEnter(HostileBehavior hostile);

   

}
