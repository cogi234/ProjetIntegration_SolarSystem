using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerationManager : MonoBehaviour
{
    private GameManager gameManager;

    [SerializeField] float minPlanetMass;
    [SerializeField] float maxPlanetMass;
    [SerializeField] float minPlanetDensity;
    [SerializeField] float maxPlanetDensity;


    private void Start()
    {
        gameManager = GetComponent<GameManager>();
    }

    public void SimpleGeneratePlanet(StellarObject sun)
    {

    }
    public void SimpleGeneratePlanet(StellarObject sun, Vector3 position)
    {

    }

}
