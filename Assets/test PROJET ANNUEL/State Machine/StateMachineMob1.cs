using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateMachineMob1 : MonoBehaviour
{
    [Header("components")]
    public Transform Player;
    public Transform CameraM;
    public NavMeshAgent Nav;
    TemplateState state;
    public IdleState idleState = new IdleState();
    public AttackState attackState = new AttackState();
    public FollowState followState = new FollowState();
    public PatrolState patrolState = new PatrolState();

    [Header("Appels")]
    public GameObject Cible;
    public bool GoTo;

    void Start()
    {
        state = idleState;
        state.StartState(this);
        Player = GameObject.Find("Player").transform;
        CameraM = GameObject.Find("Main Camera").transform;
    }


    void Update()
    {
        state.UpdateState(this);
    }

    public void ChangeState(TemplateState UseState)
    {
        state = UseState;
        state.StartState(this);
    }
}
