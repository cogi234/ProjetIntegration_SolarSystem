﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StellarObject : MonoBehaviour
{
    private GameManager gameManager;
    private Vector3 velocity;
    private float mass;
    private float size;

    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        gameManager.stellarObjectList.Add(this);
    }

    public void ApplyGravity()
    {
        foreach (StellarObject X in gameManager.stellarObjectList)
        {
            float distance = Vector3.Distance(X.transform.position, transform.position);
            float force = (mass * X.mass * gameManager.gravityConstant) / (distance * distance);
            Vector3 direction = (X.transform.position - transform.position);
            direction =direction* force;
        }
    }
    public void ApplyForce()
    {

    }
}
