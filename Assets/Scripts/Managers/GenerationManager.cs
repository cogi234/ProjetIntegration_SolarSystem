using UnityEngine;
using System;

public class GenerationManager : MonoBehaviour
{
    public static System.Random random;

    private GameManager gameManager;

    [SerializeField] float minRelativeMass = 0.001f;
    [SerializeField] float maxRelativeMass = 0.01f;
    [SerializeField] float minDensity = 1f;
    [SerializeField] float maxDensity = 6f;
    [SerializeField] float minOrbitRadiusMultiplier = 500f;
    [SerializeField] float maxOrbitRadiusMultiplier = 2000f;



    private void Awake()
    {
        gameManager = GetComponent<GameManager>();
        random = new System.Random();
    }

    public GameObject SimpleGeneratePlanet(StellarObject orbitedObject, bool flatPlane)
    {
        //In which direction will the stellar object be positionned, relative to its orbited object
        Vector3 direction = new Vector3();
        direction.x = GetRandomFloat(-1, 1);
        direction.z = GetRandomFloat(-1, 1);
        if (flatPlane)
            direction.y = 0;
        else
            direction.y = GetRandomFloat(-1, 1);

        //Then we get the position
        Vector3 position = (direction.normalized * orbitedObject.Radius * GetRandomFloat(minOrbitRadiusMultiplier, maxOrbitRadiusMultiplier)) + orbitedObject.transform.position;

        //we use the implementation 
        return SimpleGeneratePlanet(orbitedObject, flatPlane, position);
    }

    public GameObject SimpleGeneratePlanet(StellarObject orbitedObject, bool flatPlane, float orbitDistance)
    {
        //In which direction will the stellar object be positionned, relative to its orbited object
        Vector3 direction = new Vector3();
        direction.x = GetRandomFloat(-1, 1);
        direction.z = GetRandomFloat(-1, 1);
        if (flatPlane)
            direction.y = 0;
        else
            direction.y = GetRandomFloat(-1, 1);

        //Then we get the position
        Vector3 position = (direction.normalized * orbitDistance) + orbitedObject.transform.position;

        //we use the implementation 
        return SimpleGeneratePlanet(orbitedObject, flatPlane, position);
    }

    public GameObject SimpleGeneratePlanet(StellarObject orbitedObject, bool flatPlane, Vector3 position)
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

        return gameManager.CreateStellarObject("New Object", GetRandomFloat(minRelativeMass * orbitedObject.Mass, maxRelativeMass * orbitedObject.Mass), GetRandomFloat(minDensity, maxDensity), orbitedObject.Velocity + (orbitDirection * speed), position);
    }


    /// <param name="sunMass"> The mass of the sun </param>
    /// <param name="sunSize"> The volume of the sun </param>
    /// <param name="planetNumber"> The number of planets to be generated</param>
    /// <param name="minMoonNumber"> If you put e negative number, it will count all generated negative numbers as zero, shifting the probability in favor of no moons.</param>
    /// <param name="maxMoonNumber"></param>
    public void SimpleGenerateSystem(bool flatPlane, float sunMass, float sunSize, int planetNumber, int minMoonNumber = -3, int maxMoonNumber = 2)
    {
        //First, we create the sun
        StellarObject sun = gameManager.CreateSun("Sun", sunMass, sunMass / sunSize, Vector3.zero, Vector3.zero).GetComponent<StellarObject>();

        //Then, we create all planets
        StellarObject[] planets = new StellarObject[planetNumber];

        for (int i = 0; i < planetNumber; i++)
        {
            planets[i] = SimpleGeneratePlanet(sun, flatPlane).GetComponent<StellarObject>();//We create the planet
            planets[i].gameObject.name = $"Planet {i + 1}";

            //Then we create the moons for each planet
            int moonNumber = random.Next(minMoonNumber, maxMoonNumber);
            if (moonNumber > 0)
            {
                for (int j = 0; j < moonNumber; j++)
                {
                    GameObject moon = SimpleGeneratePlanet(planets[i], flatPlane);
                    moon.name = $"Moon {j + 1} of planet {i + 1}";
                }
            }
        }
    }

    [SerializeField] float minMass = 1;
    [SerializeField] float maxMass = 10;
    [SerializeField] float maxRadius = 100;
    [SerializeField] float maxSpeed = 2;
    public void GenerateFullyRandom(int bodyNumber)
    {
        for (int i = 0; i < bodyNumber; i++)
        {
            float mass = GetRandomFloat(minMass, maxMass);
            Vector3 position = new Vector3(GetRandomFloat(-1, 1), GetRandomFloat(-1, 1), GetRandomFloat(-1, 1)) * GetRandomFloat(0, maxRadius);
            Vector3 velocity = new Vector3(GetRandomFloat(-1, 1), GetRandomFloat(-1, 1), GetRandomFloat(-1, 1)) * GetRandomFloat(0, maxSpeed);
            float density = GetRandomFloat(minDensity, maxDensity);
            gameManager.CreateStellarObject($"Object {i + 1}", mass, density, velocity, position);
        }
    }

    public float GetRandomFloat(float min, float max)
    {
        return ((float)random.NextDouble() * (max - min)) + min;
    }
}
