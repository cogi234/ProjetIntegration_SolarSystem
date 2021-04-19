using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class SimulationManager : MonoBehaviour
{
    GameManager gameManager;
    public double simulatedTime = 0;
    public List<VirtualObject> virtualObjectList;
    List<int> destroyedVirtualObjects;
    float gravityConstant;
    Vector3[,] positionHistory;
    GameManager.CollisionMode collisionMode;

    bool centering;//do we center the positions?

    void Start()
    {
        gameManager = GetComponent<GameManager>();
    }

    public void Simulate(float timeStep, int stepCount)
    {
        virtualObjectList = new List<VirtualObject>();
        destroyedVirtualObjects = new List<int>();
        foreach (StellarObject X in gameManager.stellarObjectList)
        {
            virtualObjectList.Add(new VirtualObject(X));
        }
        positionHistory = new Vector3[stepCount, virtualObjectList.Count];
        gravityConstant = gameManager.gravityConstant;
        simulatedTime = gameManager.absoluteTime;
        centering = gameManager.centering;
        collisionMode = gameManager.collisionMode;

        for (int i = 0; i < stepCount; i++)
        {
            //Debug.Log("step " + i);
            SimulateTimeStep(timeStep);
            for (int j = 0; j < virtualObjectList.Count; j++)
            {
                positionHistory[i, j] = virtualObjectList[j].position;
                if (destroyedVirtualObjects.Contains(j))
                {
                    positionHistory[i, j] = positionHistory[i - 1, j];//If the object is destroyed, it doesn't move
                }
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

        DetectCollisions();

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

    private void DetectCollisions()
    {
        int count = virtualObjectList.Count;
        if (count > 1)//we only do it if we have enough objects for collisions to be possible
        {
            for (int i = 0; i < count - 1; i++)//We loop through every object except the last one
            {
                for (int j = i + 1; j < count; j++)//We loop through every object after this one
                {
                    if (!destroyedVirtualObjects.Contains(i) && !destroyedVirtualObjects.Contains(j))//we dont check destroyed objects
                    {
                        //We check to see if the distance is bigger tahn our radius + their radius, to see if objects i and j are colliding
                        if (Vector3.Distance(virtualObjectList[i].position, virtualObjectList[j].position) <= virtualObjectList[i].radius + virtualObjectList[j].radius)
                        {
                            //If they are colliding, we handle the collision
                            if (virtualObjectList[i].mass > virtualObjectList[j].mass)
                                HandleCollision(virtualObjectList[i], virtualObjectList[j]);
                            else
                                HandleCollision(virtualObjectList[j], virtualObjectList[i]);
                        }
                    }
                }
            }
        }
    }
    private void HandleCollision(VirtualObject bigObject, VirtualObject smallObject)
    {
        switch (collisionMode)
        {
            case GameManager.CollisionMode.Fusion:
                FusionCollider(bigObject, smallObject);
                break;
            case GameManager.CollisionMode.Bounce:
                BounceCollider(bigObject, smallObject);
                break;
            case GameManager.CollisionMode.None:
                break;
        }
    }
    private void FusionCollider(VirtualObject bigObject, VirtualObject smallObject)
    {
        destroyedVirtualObjects.Add(virtualObjectList.IndexOf(smallObject));//We delete the small object
        smallObject.mass = 0.0000001f;//The small object wont affect the simulation much
        bigObject.mass += smallObject.mass;//we add the mass of the small object to the big one
        bigObject.velocity = (bigObject.mass * bigObject.velocity + smallObject.mass * smallObject.velocity) / (bigObject.mass + smallObject.mass);
        bigObject.position = (bigObject.mass * bigObject.position + smallObject.mass * smallObject.position) / (bigObject.mass + smallObject.mass);
    }
    private void BounceCollider(VirtualObject objet1, VirtualObject objet2)
    {


        objet2.position = objet1.position + (objet2.position - objet1.position).normalized * (objet1.radius + objet2.radius) * 1.05f;
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
    public float radius;
    
    public VirtualObject(StellarObject X)
    {
        position = X.transform.position;
        velocity = X.Velocity;
        mass = X.Mass;
        radius = X.Radius;
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