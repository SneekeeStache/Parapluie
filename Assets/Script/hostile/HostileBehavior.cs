using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class HostileBehavior : MonoBehaviour
{
    //ce code prend en charge la gestion et le changement d'etat
    public List<GameObject> listCible = new List<GameObject>();
    HostileBaseState currentState;
    [HideInInspector] public HostileAttackState HostileAttackState = new HostileAttackState();
    [HideInInspector] public HostileChasingState HostileChasingState = new HostileChasingState();
    [HideInInspector] public HostileLookingState HostileLookingState = new HostileLookingState();
    [HideInInspector] public HostilePatroleState HostilePatroleState = new HostilePatroleState();
    [HideInInspector] public HostileDeadState HostileDeadState = new HostileDeadState();
    [HideInInspector] public HostileEatingState HostileEatingState = new HostileEatingState();
    public List<Transform> waypoints = new List<Transform>();
    public NavMeshAgent agent;
    public Transform agentTransform;
    public GameObject cible;

    [Header("Stats hostile")]

    public float health = 10;
    public float damage=5;
    //distance a la quel l'hostile attaque
    public float distanceAttaque = 20;
    //distance de detection des cible de la liste
    public float DistanceDetection = 10;
    //angle de detection des cible de la liste
    public float maxAngleDetection = 30;
    //timer pour combien de temps la cible dois sortir de la vue avant d'etre perdu
    public float durationTimerLostHostile = 2;
    //timer de temps a rester sur une position pour regarder
    public float durationTimerLooking = 3;
    //compteur de position avant de reprendre la patrouille
    public float numberlook = 6;

    public float maxHunger = 100;
    public float hunger = 100;
    public float hungerDepletion = 2;
    public float durationTimerHunger = 5;
    float timerHunger = 0;
    public bool catLike = false;



    public bool StartDead = false;
    // Start is called before the first frame update
    void Start()
    {
        //ajoute des cible a la liste
        listCible.AddRange(GameObject.FindGameObjectsWithTag("Mob1"));
        listCible.AddRange(GameObject.FindGameObjectsWithTag("Mob2"));
        listCible.AddRange(GameObject.FindGameObjectsWithTag("PlayerMonster"));

        if (!StartDead)
        {
            //definir etat patrouille au demarage
            currentState = HostilePatroleState;
            //application etat patrouille

        }
        else
        {
            currentState = HostileDeadState;
        }
        currentState.enterState(this);


    }

    // Update is called once per frame
    void Update()
    {
        //applique la fonction update aux etats
        currentState.updateState(this);
        DebugFov(maxAngleDetection, DistanceDetection, Color.red, agentTransform);
        /*if (timerHunger < durationTimerHunger)
        {
            timerHunger += Time.deltaTime;
        }
        else
        {
            hunger -= hungerDepletion;
            timerHunger = 0;
        }*/
    }

    //change d'etat
    public void changeState(HostileBaseState state)
    {
        currentState = state;
        state.enterState(this);
    }

    //affichage angle vision
    public void DebugFov(float angle, float dist, Color color, Transform objectTransform)
    {
        Vector3 extentLeft = Quaternion.AngleAxis(angle, Vector3.up) * transform.forward;
        Vector3 extentRight = Vector3.Reflect(extentLeft, objectTransform.right);
        Debug.DrawRay(objectTransform.position, extentLeft * dist, color);
        Debug.DrawRay(objectTransform.position, extentRight * dist, color);
    }

    public void takingDamage(float damage)
    {
        health -= damage;
    }
}