using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CameraRotate : MonoBehaviour
{
    [SerializeField] private float mouseSFDP = 50f;
    [SerializeField] private float minC = -70f, maxC = 80f;

    private CharacterController charController;
    public GameObject cameraFDP;
    private float xRotation = 0f;
    private Vector3 playerVelo;

    public Transform ParapluieTransform;

    void Start()
    {
        //cameraFDP = Camera.main;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        //float horizontal = Input.GetAxis("HorizontalCamera");
        //float Vertical = Input.GetAxis("VerticalCamera");
        //Vector2 axisVector = Vector2.zero;
        //axisVector.x = Input.GetAxis("CameraHorizontal");
        //axisVector.y= Input.GetAxis("CameraVertical");
        //print(axisVector);
        float mouseX = Input.GetAxis("Mouse X") * mouseSFDP * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSFDP * Time.deltaTime;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, minC, maxC);

        transform.localRotation = Quaternion.Euler(xRotation, transform.localRotation.eulerAngles.y, 0);
        transform.Rotate(Vector3.up * mouseX);
        //transform.Rotate(Vector3.right * Vertical);
        //transform.Rotate(Vector3.up * horizontal);



        transform.DOMove(ParapluieTransform.position, 01f);
        if( Input.GetKeyDown(KeyCode.F5)) SceneManager.LoadScene("test");

        /*if (Input.mouseScrollDelta.y > 0f)
        {
            cameraFDP.transform.position += transform.forward;
        }
        if (Input.mouseScrollDelta.y < 0f)
        {
            cameraFDP.transform.position += -transform.forward;
        }*/
        //cameraFDP.transform.position = Vector3.ClampMagnitude(new Vector3() );

        /*if(Input.mouseScrollDelta.y > 0f && cameraFDP.transform.position.z < -20){
            cameraFDP.transform.position += transform.forward;
        }
        if(Input.mouseScrollDelta.y < 0f && cameraFDP.transform.position.z > -50){
            cameraFDP.transform.position += -transform.forward;
        }*/
        //cameraFDP.transform.position = new Vector3(cameraFDP.transform.position.x, cameraFDP.transform.position.y, cameraFDP.transform.position.z * transform.forward);
    }
}
