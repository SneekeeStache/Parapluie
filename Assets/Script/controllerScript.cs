using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controllerScript : MonoBehaviour
{
    [Header("caracteristique")]
    [SerializeField] float Speed = 12;
    [SerializeField] float acceleration = 60;
    [SerializeField] float friction = 50;
    [SerializeField] float airFriction = 10;
    [SerializeField] float jumpForce = 8;
    [SerializeField] float gravity = 9.8f;
    [Header("parametre detection")]
    [SerializeField] float radiusSphere = 3;
    [Header("Souris")]
    [SerializeField] float CameraSensitivity = 50f;
    [Header("componente")]
    [SerializeField] Transform camera;
    [SerializeField] Transform cameraPosition;
    [SerializeField] CharacterController CC;
    [SerializeField] Transform sphereColisionOrigin;
    [Header("test perso")]
    [SerializeField] float diviseurVitesse=2;

    // variable a ne pas touché
    Vector3 velocity = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        Speed=Speed/60;
        acceleration=acceleration/60;
        friction=friction/60;
        airFriction=airFriction/60;
        jumpForce=jumpForce/60;
        gravity=gravity/32.66f;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 inputVector = getinputVector();
        Vector3 direction = getDirection(inputVector);
        applyMouvement(direction);
        applyFriction(direction);
        applyGravity();
        jump();
        CC.Move(velocity);
        camera.position=cameraPosition.position;
        
    }
    void FixedUpdate()
    {
        
    }

    Vector3 getinputVector()
    {
        Vector3 inputVector = Vector3.zero;
        inputVector.x = Input.GetAxisRaw("Horizontal");
        inputVector.z = Input.GetAxisRaw("Vertical");
        return inputVector;
    }
    Vector3 getDirection(Vector3 inputVector)
    {
        Vector3 direction = Vector3.zero;
        direction = (transform.forward * inputVector.z) + (transform.right * inputVector.x);
        return direction;
    }

    void applyMouvement(Vector3 direction)
    {
        if (direction != Vector3.zero)
        {
            velocity.x = Mathf.MoveTowards(velocity.x, direction.x * Speed, acceleration * Time.deltaTime);
            velocity.z = Mathf.MoveTowards(velocity.z, direction.z * Speed, acceleration * Time.deltaTime);
        }
    }
    void applyFriction(Vector3 direction){
        if(direction == Vector3.zero){
            if(isOnGround()){
                velocity = Vector3.MoveTowards(velocity,Vector3.zero,friction *Time.deltaTime);
            }else{
                velocity.x = Mathf.MoveTowards(velocity.x,0,airFriction *Time.deltaTime);
                velocity.z = Mathf.MoveTowards(velocity.z,0,airFriction *Time.deltaTime);
            }
        }
    }
    void applyGravity()
    {
        if(isOnGround()){
            velocity.y=0;
        }else{
            velocity.y -= gravity * Time.deltaTime;
        }
        
    }
    void jump(){
        if(Input.GetButtonDown("Jump") && isOnGround()){
            velocity.y = jumpForce;
            
        }
        if(Input.GetButtonUp("Jump") && velocity.y > jumpForce / 2){
            velocity.y = jumpForce / 2;
        }
    }

    bool isOnGround()
    {
        bool isOnGround = false;
        if(Physics.CheckSphere(sphereColisionOrigin.position,radiusSphere,7)){
            isOnGround=true;
        }else{
            isOnGround=false;
        }
        return isOnGround;
    }
    
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(sphereColisionOrigin.position,radiusSphere);
    }
}


