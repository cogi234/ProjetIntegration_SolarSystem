using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private GameManager gameManager;
    [SerializeField] float mouseSensitivity;
    [SerializeField] float movementSpeed;
    [SerializeField] float reframingFOV;

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;
    }

    void Update()
    {
        //Lock and unlock mouse
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (Cursor.lockState == CursorLockMode.None) { Cursor.lockState = CursorLockMode.Locked; }
            else { Cursor.lockState = CursorLockMode.None; }
        }

        //control mouse and WASD movement
        if(Cursor.lockState == CursorLockMode.Locked)
        {
            transform.Rotate(Input.GetAxis("Mouse Y") * -mouseSensitivity * Time.deltaTime, Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime, 0, Space.Self);
        }

        transform.Translate(Input.GetAxis("Horizontal") * movementSpeed * Time.deltaTime, 0, Input.GetAxis("Vertical") * movementSpeed * Time.deltaTime, Space.Self);



        //auto adjust camera to show the whole solar system (paired with Gamemanager)
        if (Input.GetKey(KeyCode.R)) 
        {
            float MaxDistance = 15;
            
            foreach(StellarObject A in gameManager.stellarObjectList)
            {
                if (A.transform.position.magnitude > MaxDistance) { MaxDistance = A.transform.position.magnitude; }
            }


            transform.position = new Vector3(0,MaxDistance*reframingFOV, 0);
            transform.rotation = Quaternion.Euler(new Vector3(90,0,0));
        }
    }
}
