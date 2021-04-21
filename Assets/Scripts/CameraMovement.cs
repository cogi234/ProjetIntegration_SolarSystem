using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    enum CameraMode
    {
        Free, Centered
    }

    private CameraMode cameraMode = CameraMode.Centered;
    private Transform centeredFocus;
    private float distanceFromFocus = 15;
    private Vector3 angleFromFocus = Vector3.zero;

    private GameManager gameManager;
    private UIManager uiManager;
    [SerializeField] float mouseSensitivity;
    [SerializeField] float movementSpeed = 5;
    [SerializeField] float accelerationConstant = 0.5f;
    [SerializeField] float startingSpeed = 5;
    [SerializeField] float reframingFOV;
    [SerializeField] float zoomSensitivity;

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        uiManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<UIManager>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;

        centeredFocus = new GameObject("Camera Focus").transform;
        angleFromFocus = Vector3.one;
    }

    void Update()
    {
        //We put the focus on the selected object
        if (uiManager.SelectedObject != null)
            centeredFocus.position = uiManager.SelectedObject.transform.position;

        //Lock and unlock mouse
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (Cursor.lockState == CursorLockMode.None) 
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            else 
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }
        //Switch between camera modes
        if (Input.GetKeyDown(KeyCode.F))
        {
            switch (cameraMode)
            {
                case CameraMode.Free:
                    cameraMode = CameraMode.Centered;
                    distanceFromFocus = Vector3.Distance(centeredFocus.position, transform.position);
                    centeredFocus.LookAt(transform);
                    angleFromFocus = centeredFocus.eulerAngles;
                    break;
                case CameraMode.Centered:
                    cameraMode = CameraMode.Free;
                    break;
                default:
                    break;
            }
        }

        switch (cameraMode)
        {
            case CameraMode.Free:
                FreeCameraProcess();
                break;
            case CameraMode.Centered:
                CenteredCameraProcess();
                break;
            default:
                break;
        }
       
    }

    void FreeCameraProcess()
    {
        //control mouse movement
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            transform.Rotate(Input.GetAxis("Mouse Y") * -mouseSensitivity, Input.GetAxis("Mouse X") * mouseSensitivity, 0, Space.Self);
        }
        //control WASD movement
        transform.Translate(Input.GetAxis("Horizontal") * movementSpeed * Time.deltaTime, 0, Input.GetAxis("Vertical") * movementSpeed * Time.deltaTime, Space.Self);
        if (Input.GetAxis("Horizontal") == 1 || Input.GetAxis("Vertical") == 1) 
        {
            movementSpeed += accelerationConstant * Time.deltaTime;
        }
        else
        {
            movementSpeed = startingSpeed;
        }

        //auto adjust camera to show the whole solar system (paired with Gamemanager)
        if (Input.GetKey(KeyCode.R))
        {
            float MaxDistance = 15;

            foreach (StellarObject A in gameManager.stellarObjectList)
            {
                if (A.transform.position.magnitude > MaxDistance) { MaxDistance = A.transform.position.magnitude; }
            }


            transform.position = new Vector3(0, MaxDistance * reframingFOV, 0);
            transform.rotation = Quaternion.Euler(new Vector3(90, 0, 0));
        }
    }

    void CenteredCameraProcess()
    {
        //control mouse movement
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            angleFromFocus += new Vector3(Input.GetAxis("Mouse Y") * mouseSensitivity, Input.GetAxis("Mouse X") * mouseSensitivity, 0);

            distanceFromFocus *= 1 + (-Input.GetAxis("Mouse ScrollWheel") * zoomSensitivity);
        }

        //We use the angle and distance from the focus to move the camera
        centeredFocus.rotation = Quaternion.Euler(angleFromFocus);
        transform.position = centeredFocus.position + (centeredFocus.forward * distanceFromFocus);
        transform.LookAt(centeredFocus);
    }
}


