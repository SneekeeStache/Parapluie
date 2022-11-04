using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 7f;
    [SerializeField] private float speedMAJ = 10f;
    [SerializeField] private float speedNOMAJ = 4f;
    [SerializeField] public float mainControllerSen = 50f;
    [SerializeField] private float minC = -70f, maxC = 80f;
    //public static PlayerController instance;
    //public MenuController FDPLASENSI;

    private CharacterController charController;
    private Camera cameraFDP;
    private float xRotation = 0f;
    private Vector3 playerVelo;
    public float STAMINALAPTNDETARACE = 100;
    private bool peutCourir = true;
    private int currentHealth;
    public float clef1 = 0f;
    public bool canUse = false;
    public GameObject CurseurCanUse;
    public GameObject ennemi;
    //public HealthBar healthBar;
    public bool vueFPS = true;
    private bool triggerOnce;

    void Start()
    {
        CurseurCanUse.SetActive(false);
        cameraFDP = Camera.main;

        charController = GetComponent<CharacterController>();
        if (charController == null)
        {
            
        }
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void GetLASENSISARACE()
    {
        //mainControllerSen = MenuController.instance.mainControllerSen;
    }



    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            transform.localScale = new Vector3 (1f,0.2f,1f);
        }
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            transform.localScale = new Vector3 (1f,0.7f,1f);
        }

        if (canUse == true)
        {

            if (triggerOnce == true)
            {
                CurseurCanUse.SetActive(true);
                CurseurCanUse.GetComponent<Animator>().Play("CursorTake", 0, 0.0f);
            }
            triggerOnce = false;
        }
        else
        {
            CurseurCanUse.SetActive(false);
            triggerOnce = true;
        }

            
        

        Ray ray = new Ray(transform.position, Vector3.forward);
        //Debug.DrawRay(transform.position, Vector3.forward, Color.blue, Mathf.Infinity);
        RaycastHit hit;

        Physics.Raycast(transform.position, cameraFDP.transform.forward, out hit, Mathf.Infinity);
        if (hit.transform.gameObject.CompareTag("Use"))
        {
            //Debug.DrawRay(transform.position, cameraFDP.transform.forward * hit.distance, Color.yellow);
            canUse = true;
        }
        else
            canUse = false;

        GetLASENSISARACE();
        //mainControllerSen = FDPLASENSI.mainControllerSen;
        //healthBar.SetHealth((int)STAMINALAPTNDETARACE);

        float horizontal = Input.GetAxis("Horizontal");
        float Vertical = Input.GetAxis("Vertical");


        if (Input.GetKey(KeyCode.LeftShift) && peutCourir == true)
        {
            speed = speedMAJ;
            STAMINALAPTNDETARACE -= 10 * Time.deltaTime;
            //ennemi.GetComponent<Detection>().SeuilDetection360 = 20f;
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
            //ennemi.GetComponent<Detection>().SeuilDetection360 = 10f;
            if (STAMINALAPTNDETARACE >= 100f)
            {
                STAMINALAPTNDETARACE = 100;
            }
        }
        if (Input.GetKey(KeyCode.LeftShift) && peutCourir == false)
        {
            speed = speedNOMAJ;
            STAMINALAPTNDETARACE += 10 * Time.deltaTime;
            //ennemi.GetComponent<Detection>().SeuilDetection360 = 5f;
            if (STAMINALAPTNDETARACE >= 100f)
            {
                STAMINALAPTNDETARACE = 100;
            }
        }


        Vector3 movement = transform.forward * Vertical + transform.right * horizontal;
        movement = Vector3.ClampMagnitude(movement, 1f);
        charController.Move(movement * Time.deltaTime * speed);
        if (vueFPS == true)
        {
            float mouseX = Input.GetAxis("Mouse X") * mainControllerSen * 10 * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mainControllerSen * 10 * Time.deltaTime;
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, minC, maxC);
            cameraFDP.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
            transform.Rotate(Vector3.up * mouseX);
        }


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


   // continue ça skip un passage de boucle
   // break ça arrete la loop en cours
   // return ça arrete de lire la méthode