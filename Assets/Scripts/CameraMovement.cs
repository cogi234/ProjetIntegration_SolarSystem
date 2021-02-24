using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private GameManager gameManager;
    [SerializeField] float mouseSensitivity;
    [SerializeField] float movementSpeed;


    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        

    }
    void Update()
    {
        transform.Translate(Input.GetAxis("Horizontal")*movementSpeed, Input.GetAxis("Vertical")*movementSpeed, 0);
        transform.Rotate(Input.GetAxis("Mouse Y")*mouseSensitivity, Input.GetAxis("Mouse X")*mouseSensitivity, 0);


        
        if (Input.GetKeyDown("space")) 
        {
            float xStellarObjectSum = 0;
            float zStellarObjectSum = 0;
            
            //a terminer
            foreach (StellarObject A in gameManager.stellarObjectList)
            {
                xStellarObjectSum += A.transform.position.x;
                zStellarObjectSum += A.transform.position.z;
            }
            float X = xStellarObjectSum / gameManager.stellarObjectList.Count;
            float Z = zStellarObjectSum / gameManager.stellarObjectList.Count;
            float Y = Mathf.Max(X, Z);
            transform.position = new Vector3(X, Y, Z);
        }
    }
}
