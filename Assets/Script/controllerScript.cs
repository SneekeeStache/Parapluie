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
    [SerializeField] float vitesseSpinAction=10;
    [SerializeField] float dashForce=0.5f;
    [Header("parametre detection")]
    [SerializeField] float radiusSphere = 3;
    [Header("Souris")]
    [SerializeField] float CameraSensitivity = 50f;
    [Header("componente")]
    [SerializeField] Transform cameraHolder;
    [SerializeField] Transform cameraPosition;
    [SerializeField] GameObject cameraOrientation;
    [SerializeField] CharacterController CC;
    [SerializeField] Transform sphereColisionOrigin;
    [SerializeField] GameObject projectil;
    [Header("test perso")]


    // variable a ne pas touché
    Vector3 velocity = Vector3.zero;
    float mouseX;
    float mouseY;
    float xRotation;
    float yRotation;

    // Start is called before the first frame update
    void Start()
    {
        //je divise les valeur sinon les valeur a rentrer doivent etre tres petite
        Speed=Speed/60;
        acceleration=acceleration/60;
        friction=friction/60;
        airFriction=airFriction/60;
        jumpForce=jumpForce/60;
        gravity=gravity/60;
        CameraSensitivity=CameraSensitivity/60;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.M)){
            if(Cursor.lockState == CursorLockMode.Locked){
                Cursor.lockState=CursorLockMode.None;
            }else{
                Cursor.lockState=CursorLockMode.Locked;
            }
        }
        cameraControle();
        Vector3 inputVector = getinputVector();
        Vector3 direction = getDirection(inputVector);
        applyMouvement(direction);
        applyFriction(direction);
        applyGravity();
        jump();
        CC.Move(velocity);

        if(Input.GetButton("action5")){
            yRotation += vitesseSpinAction;
        }
        if(Input.GetButtonDown("action1")){
            Instantiate(projectil,cameraPosition.transform.position,Quaternion.Euler(cameraHolder.rotation.eulerAngles.x,cameraOrientation.transform.eulerAngles.y,0));
        }
        if(Input.GetButtonDown("action2")){
            velocity*= dashForce;
            print("test");
        }
        
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
        direction = (cameraOrientation.transform.forward * inputVector.z) + (cameraOrientation.transform.right * inputVector.x);
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

    void cameraControle(){
        cameraHolder.position=cameraPosition.position;

        mouseX = Input.GetAxisRaw("Mouse X");
        mouseY = Input.GetAxisRaw("Mouse Y");

        yRotation += mouseX * CameraSensitivity;
        xRotation -= mouseY * CameraSensitivity;

        xRotation = Mathf.Clamp(xRotation,-90f,90f);

        cameraHolder.transform.rotation = Quaternion.Euler(xRotation, yRotation,0);
        cameraOrientation.transform.rotation = Quaternion.Euler(0,yRotation,0);
    }
}


