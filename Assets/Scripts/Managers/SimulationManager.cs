using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimulationManager : MonoBehaviour
{
    GameManager gameManager;
    public double simulatedTime = 0;
    public List<VirtualObject> virtualObjectList;
    float gravityConstant;
    Vector3[,] positionHistory;

    bool centering;//do we center the positions?

    void Start()
    {
        gameManager = GetComponent<GameManager>();
    }

    public void Simulate(float timeStep, int stepCount)
    {
        virtualObjectList = new List<VirtualObject>();
        foreach (StellarObject X in gameManager.stellarObjectList)
        {
            virtualObjectList.Add(new VirtualObject(X));
        }
        positionHistory = new Vector3[stepCount, virtualObjectList.Count];
        gravityConstant = gameManager.gravityConstant;
        simulatedTime = gameManager.absoluteTime;
        centering = gameManager.centering;

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

        //Centering the system in the same way as the gamemanager, to have a prediction that's synchronized
        if (virtualObjectList.Count > 0 && centering)
        {
            //Centering the system:
            float X, Y, Z;
            //auto adjust camera to show the whole solar system (paired with CameraMovement)
            float xStellarObjectSum = 0;
            float yStellarObjectSum = 0;
            float zStellarObjectSum = 0;

            foreach (VirtualObject A in virtualObjectList)
            {
                xStellarObjectSum += A.position.x;
                yStellarObjectSum += A.position.y;
                zStellarObjectSum += A.position.z;
            }
            X = xStellarObjectSum / virtualObjectList.Count;
            Y = yStellarObjectSum / virtualObjectList.Count;
            Z = zStellarObjectSum / virtualObjectList.Count;

            foreach (VirtualObject A in virtualObjectList)
            {
                A.position -= new Vector3(X, Y, Z);
            }
        }
    }


    //Cette fonction est prise d'une question sur stackoverflow:       https://stackoverflow.com/questions/16636019/how-to-get-1d-column-array-and-1d-row-array-from-2d-array-c-net
    public Vector3[] GetPositionHistoryOfObject(int index)
    {
        int historyLength = positionHistory.GetLength(0);
        Vector3[] history = new Vector3[historyLength];

        for (int i = 0; i < historyLength; i++)
        {
            history[i] = positionHistory[i, index];
        }

        return history;
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