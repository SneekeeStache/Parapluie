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
    [SerializeField] float distanceRaycast = 5;
    [SerializeField] float radiusSphere = 3;
    [Header("Souris")]
    [SerializeField] float CameraSensitivity = 50f;
    [Header("componente")]
    [SerializeField] GameObject camera;
    [SerializeField] CharacterController CC;
    [SerializeField] Transform sphereColisionOrigin;

    // variable a ne pas touché
    Vector3 velocity = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(isOnGround());
        Vector3 inputVector = getinputVector();
        Vector3 direction = getDirection(inputVector);
        applyMouvement(direction);
    }
    void FixedUpdate()
    {
        CC.Move(velocity);
    }

    Vector3 getinputVector()
    {
        Vector3 inputVector = Vector3.zero;
        inputVector.x = Input.GetAxis("Horizontal");
        inputVector.z = Input.GetAxis("Vertical");
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
    void applyGravity()
    {

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


