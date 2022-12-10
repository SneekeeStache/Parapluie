using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class cam_mouvement : MonoBehaviour
{
    public GameObject GameObject_call;
    public GameObject bound_camera_Up;
    BoxCollider boundsColliderUp;
     public GameObject bound_camera_down;
    BoxCollider boundsColliderDown;

     public GameObject bound_camera_left;
    BoxCollider boundsColliderLeft;

     public GameObject bound_camera_right;
    BoxCollider boundsColliderRight;

    GameObject_Call GOCscript;
    public float sensitivityCamera = 2;

    public float maxCamSize = 10f;

    public float minCamSize = 3f;

    Vector3 mousePosition;
    // Start is called before the first frame update
    void Start()
    {
        GOCscript = GameObject_call.GetComponent<GameObject_Call>();
        boundsColliderUp = bound_camera_Up.GetComponent<BoxCollider>();
        boundsColliderDown = bound_camera_down.GetComponent<BoxCollider>();
        boundsColliderLeft = bound_camera_left.GetComponent<BoxCollider>();
        boundsColliderRight = bound_camera_right.GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {


        if (Input.GetAxis("Mouse ScrollWheel") < 0 && Camera.main.orthographicSize < 10f)
        {
            Camera.main.orthographicSize += 0.5f;
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0 && Camera.main.orthographicSize > 3f)
        {
            Camera.main.orthographicSize -= 0.5f;
        }
        mousePosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        if (!boundsColliderRight.bounds.Contains(transform.position) && ((mousePosition.x < 1.1f && mousePosition.x > 0.9f && !GOCscript.is_mouse_over_ui()) || Input.GetKey(KeyCode.D)))
        {
            transform.position += Vector3.right * sensitivityCamera * Time.deltaTime;
        }
        else if (!boundsColliderLeft.bounds.Contains(transform.position) &&((mousePosition.x < 0.1f && mousePosition.x > -0.1f && !GOCscript.is_mouse_over_ui()) || Input.GetKey(KeyCode.Q)))
        {
            transform.position += Vector3.left * sensitivityCamera * Time.deltaTime;
        }
        if (!boundsColliderUp.bounds.Contains(transform.position) && ((mousePosition.y < 1.1f && mousePosition.y > 0.9f && !GOCscript.is_mouse_over_ui()) || Input.GetKey(KeyCode.Z)))
        {
            transform.position += Vector3.forward * sensitivityCamera * Time.deltaTime;
        }
        else if (!boundsColliderDown.bounds.Contains(transform.position) && ((mousePosition.y < 0.1f && mousePosition.y > -0.1f && !GOCscript.is_mouse_over_ui()) || Input.GetKey(KeyCode.S)))
        {
            transform.position += Vector3.back * sensitivityCamera * Time.deltaTime;
        }


    }


}


