using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICreateMenu : MonoBehaviour
{
    GenerationManager generationManager;
    GameManager gameManager;
    UIManager uiManager;

    private void Start()
    {
        uiManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<UIManager>();//We find the uiManager
        generationManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GenerationManager>();//We find the generationManager
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();//We find the gameManager
    }

    //Procedural Creation:
    public bool ProcGenFlatPlane { get; set; }
    public string ProcGenOrbitDistance { get; set; }

    public void GeneratePlanet()
    {
        generationManager.SimpleGeneratePlanet(uiManager.selectedObject, ProcGenFlatPlane, float.Parse(ProcGenOrbitDistance));
    }
}
