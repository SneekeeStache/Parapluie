using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionIndicator : MonoBehaviour
{
    //center est fait pour utiliser le meme code pour tout afin que le gameobject direction suivent le joueur
    [Header("ne pas choisir si il sagit d'un trigger")]
    [SerializeField] bool center;
    [Header("choisir une direction pour un trigger")]
    //determine la direction du Parapluie avec les trigger
    [SerializeField] bool forward;
    [SerializeField] bool backward;
    [SerializeField] bool right;
    [SerializeField] bool left;
    [SerializeField] bool up;
    [SerializeField] bool down;
    [Header("ajouter si centre est cocher")]
    // prend la position du joueur
    [SerializeField] Transform player;
    [Header("script du player")]
    [SerializeField] Player playerScript;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    private void FixedUpdate() {
        if(center){
            transform.position=player.position;
        }
    }

    private void OnTriggerStay(Collider other) {
        if(forward){
            playerScript.forward=true;
        }
        if(backward){
            playerScript.backward=true;
        }
        if(right){
            playerScript.right=true;
        }
        if(left){
            playerScript.left=true;
        }
        if(up){
            playerScript.up=true;
        }
        if(down){
            playerScript.down=true;
        }
    }
    private void OnTriggerExit(Collider other) {
        if(forward){
            playerScript.forward=false;
        }
        if(backward){
            playerScript.backward=false;
        }
        if(right){
            playerScript.right=false;
        }
        if(left){
            playerScript.left=false;
        }
        if(up){
            playerScript.up=false;
        }
        if(down){
            playerScript.down=false;
        }
    }
}
