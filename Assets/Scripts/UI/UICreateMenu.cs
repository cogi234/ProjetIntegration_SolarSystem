﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

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
        generationManager.SimpleGeneratePlanet(uiManager.SelectedObject, ProcGenFlatPlane, float.Parse(ProcGenOrbitDistance));
    }

    //Plain Creation:
    [SerializeField] UIImageFlip pauseButton;
    public void CreatePlanet()
    {
        Vector3 pos = new Vector3(generationManager.GetRandomFloat(-10, 10), generationManager.GetRandomFloat(-10, 10), generationManager.GetRandomFloat(-10, 10));

        gameManager.CreateStellarObject("New Planet", 1, 1, Vector3.zero, pos);

        gameManager.paused = true;
        pauseButton.State = false;
    }
    public void CreateSun()
    {
        Vector3 pos = new Vector3(generationManager.GetRandomFloat(-10, 10), generationManager.GetRandomFloat(-10, 10), generationManager.GetRandomFloat(-10, 10));

        gameManager.CreateSun("New Sun", 1, 1, Vector3.zero, pos);

        gameManager.paused = true;
        pauseButton.State = false;
    }
}
