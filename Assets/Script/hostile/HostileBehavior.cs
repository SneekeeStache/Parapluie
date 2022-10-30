using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class HostileBehavior : MonoBehaviour
{
    //ce code prend en charge la gestion et le changement d'etat
    public List<GameObject> listCible = new List<GameObject>();
    HostileBaseState currentState;
    public attackState attackState = new attackState();
    public chasingState chasingState = new chasingState();
    public lookingState lookingState = new lookingState();
    public patroleState patroleState = new patroleState();
    public deadState deadState = new deadState();
    public List<Transform> waypoints = new List<Transform>();
    public NavMeshAgent agent;
    public Transform agentTransform;
    public GameObject cible;

    [Header("Stats hostile")]
    public float distanceAttaque=20;
    public float DistanceDetection=10;
    public float maxAngleDetection=30;
    public float durationTimerLostHostile=2;

    public float durationTimerLooking=3;
    public float numberlook=6;

    // Start is called before the first frame update
    void Start()
    {
        listCible.AddRange(GameObject.FindGameObjectsWithTag("pacifiqueMonster"));
        listCible.AddRange(GameObject.FindGameObjectsWithTag("PlayerMonster"));
        currentState = patroleState;
        currentState.enterState(this);
        
    }

    // Update is called once per frame
    void Update()
    {
        currentState.updateState(this);
    }

    public void changeState(HostileBaseState state){
        currentState = state;
        state.enterState(this);
    }
}
