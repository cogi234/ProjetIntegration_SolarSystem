using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class NBody : MonoBehaviour
{
    private GameManager gameManager;
    private StellarObject stellarObject;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        stellarObject = GameObject.FindGameObjectWithTag("GameController").GetComponent<StellarObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // A(Xn) = G * m * r / abs(r)pow3
    // acc (objet Xn) = ConstGrav * MassObjetStellaire * distanceObjet
    //                    / abs(distanceObjetStellaire)pow3
    float acceleration = ;

    // Vn+1 = Vn + h * An
    // h = delta temps

    // Xn+1 = Xn + h * Vn+1
   
}

