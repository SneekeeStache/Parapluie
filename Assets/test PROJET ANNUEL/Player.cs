using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 7f;
    [SerializeField] private float speedMAJ = 10f;
    [SerializeField] private float speedNOMAJ = 5f;
    [SerializeField] private float mouseSFDP = 50f;
    [SerializeField] private float minC = -70f, maxC = 80f;
    
    private CharacterController charController;
    private Camera cameraFDP;
    private float xRotation = 0f;
    private Vector3 playerVelo;
    public float STAMINALAPTNDETARACE = 100;
    private bool peutCourir = true;
    private int currentHealth;
    public float clef1 = 0f;

    public bool LockCurseur;
    //public HealthBar healthBar;


    void Start()
    {
        cameraFDP = Camera.main;

        charController = GetComponent<CharacterController>();
        if (charController == null)
        {
            
        }
        Cursor.lockState = CursorLockMode.Locked;
    }






    void Update()
    {

        if (LockCurseur) Cursor.lockState = CursorLockMode.Locked;
        else Cursor.lockState = CursorLockMode.Confined;
        //healthBar.SetHealth((int)STAMINALAPTNDETARACE);

        float horizontal = Input.GetAxis("Horizontal");
        float Vertical = Input.GetAxis("Vertical");


        if (Input.GetKey(KeyCode.LeftShift) && peutCourir == true)
        {
            speed = speedMAJ;
            STAMINALAPTNDETARACE -= 10 * Time.deltaTime;
        }

        if (STAMINALAPTNDETARACE <= 0f)
        {
            peutCourir = false;
            speed = speedNOMAJ;
        }
        if (STAMINALAPTNDETARACE >= 5f)
        {
            peutCourir = true;
        }

        if (!Input.GetKey(KeyCode.LeftShift) )
        {
            speed = speedNOMAJ;
            STAMINALAPTNDETARACE += 5 * Time.deltaTime;

            if (STAMINALAPTNDETARACE >= 100f)
            {
                STAMINALAPTNDETARACE = 100;
            }
        }
        if (Input.GetKey(KeyCode.LeftShift) && peutCourir == false)
        {
            speed = speedNOMAJ;
            STAMINALAPTNDETARACE += 10 * Time.deltaTime;

            if (STAMINALAPTNDETARACE >= 100f)
            {
                STAMINALAPTNDETARACE = 100;
            }
        }


        Vector3 movement = transform.forward * Vertical + transform.right * horizontal;
        movement = Vector3.ClampMagnitude(movement, 1f);
        charController.Move(movement * Time.deltaTime * speed);

        float mouseX = Input.GetAxis("Mouse X") * mouseSFDP * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSFDP * Time.deltaTime;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, minC, maxC);

        cameraFDP.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        transform.Rotate(Vector3.up * mouseX);


    }






    private void FixedUpdate()
    {
        
        if (charController.isGrounded)
        {
            playerVelo.y = 0f;
        }
        else
        {
            playerVelo.y += -9.81f * Time.deltaTime;
            charController.Move(playerVelo * Time.deltaTime);
        }
    }
}
