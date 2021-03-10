using UnityEngine;
using System;

public class GenerationManager : MonoBehaviour
{
    public static System.Random random;

    private GameManager gameManager;

    [SerializeField] float minRelativeMass = 0.02f;
    [SerializeField] float maxRelativeMass = 0.1f;
    [SerializeField] float minDensity = 1f;
    [SerializeField] float maxDensity = 6f;



    private void Start()
    {
        gameManager = GetComponent<GameManager>();
        random = new System.Random();
    }

    public void SimpleGeneratePlanet(StellarObject orbitedObject, bool flatPlane, float minOrbit, float maxOrbit)
    {
        //In which direction will the stellar object be positionned, relative to its orbited object
        Vector3 direction = new Vector3();
        direction.x = GetRandomFloat(-1, 1);
        direction.z = GetRandomFloat(-1, 1);
        if (flatPlane)
            direction.y = 0;
        else
            direction.y = GetRandomFloat(-1, 1);

        //Then we choose the distance and get the position
        float distance = GetRandomFloat(minOrbit, maxOrbit);

        Vector3 position = (direction.normalized * distance) + orbitedObject.transform.position;

        //we use the implementation 
        SimpleGeneratePlanet(orbitedObject, flatPlane, position);
    }

    public void SimpleGeneratePlanet(StellarObject orbitedObject, bool flatPlane, Vector3 position)
    {
        //the direction from the orbited object to the object we will create
        Vector3 direction = position - orbitedObject.transform.position;
        //another direction for the cross product
        Vector3 perpendicular = new Vector3(0, 1, 0);

        if (!flatPlane)
            perpendicular = new Vector3(GetRandomFloat(-1, 1), GetRandomFloat(-1, 1), GetRandomFloat(-1, 1));

        //we calculate the direction of orbit with a crossproduct, to be perpendicular to the object we are orbiting
        Vector3 orbitDirection = Vector3.Cross(direction, perpendicular).normalized;


        //Then we need to calculate a speed to have a stable orbit
        float speed = Mathf.Sqrt(gameManager.gravityConstant * orbitedObject.Mass / direction.magnitude);

        gameManager.CreateStellarObject("New Object", GetRandomFloat(minRelativeMass * orbitedObject.Mass, maxRelativeMass * orbitedObject.Mass), GetRandomFloat(minDensity, maxDensity), orbitedObject.Velocity + (orbitDirection * speed), position);
    }


    float GetRandomFloat(float min, float max)
    {
        return ((float)random.NextDouble() * (max - min)) + min;
    }
}
