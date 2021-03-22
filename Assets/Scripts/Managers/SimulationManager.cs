﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimulationManager : MonoBehaviour
{
    GameManager gameManager;
    public double simulatedTime = 0;
    private List<VirtualObject> virtualObjectList;
    float gravityConstant;
    Vector3[,] positionHistory;

    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }

    void Simulate(float timeStep, int stepCount)
    {
        positionHistory = new Vector3[stepCount, virtualObjectList.Count];
        gravityConstant = gameManager.gravityConstant;
        simulatedTime = gameManager.absoluteTime;
        virtualObjectList = new List<VirtualObject>();
        foreach (StellarObject X in gameManager.stellarObjectList)
        {
            virtualObjectList.Add(new VirtualObject(X));
        }

        for (int i = 0; i < stepCount; i++)
        {
            SimulateTimeStep(timeStep);
            for (int j = 0; j < virtualObjectList.Count; j++)
            {
                positionHistory[i, j] = virtualObjectList[j].position;
            }
        }
    }
    void SimulateTimeStep(float timeStep)
    {
        simulatedTime += timeStep;

        foreach (VirtualObject A in virtualObjectList)
        {
            A.ApplyGravity(timeStep, virtualObjectList, gravityConstant);
        }

        foreach (VirtualObject A in virtualObjectList)
        {
            A.ApplyVelocity(timeStep);

        }
    }
}
public class VirtualObject
{
    public Vector3 position;
    public Vector3 velocity;
    public float mass;
    
    public VirtualObject(StellarObject X)
    {
        position = X.transform.position;
        velocity = X.Velocity;
        mass = X.Mass;
    }
    public void ApplyGravity(float time, List<VirtualObject> list, float gravityConstant)
    {
        Vector3 totalForce = new Vector3();
        foreach (VirtualObject X in list)
        {
            if (X != this)
            {
                float GravityForce = (mass * X.mass * gravityConstant)
                    / Mathf.Pow(Vector3.Distance(X.position, position), 2);
                Vector3 direction = (X.position - position).normalized * GravityForce;
                ApplyForce(direction, time);
                totalForce += direction;
            }
        }
    }

    //cree un vecteur force a partir dun un objet stellaire
    public void ApplyForce(Vector3 force, float time)
    {
        //Vector3 acc = force / Mass;
        velocity += (force / mass) * time;
    }

    //applique un vecteur force a partir dun objet stellaire
    public void ApplyVelocity(float time)
    {
        position += velocity * time;
    }
}