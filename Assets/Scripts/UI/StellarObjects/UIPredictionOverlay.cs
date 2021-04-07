using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPredictionOverlay : MonoBehaviour
{
    public Vector3[] positions;
    private LineRenderer line;
    UIManager uiManager;
    GameManager gameManager;

    private void Start()
    {
        line = GetComponent<LineRenderer>();
        uiManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<UIManager>();//We find the uiManager
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();//We find the gameManager
    }

    //We only need to update the display on fixed updates, because physics are only updated on fixed updates
    private void FixedUpdate()
    {
        int numberOfPositionsToDisplay = positions.Length - Mathf.FloorToInt((float)(gameManager.absoluteTime - uiManager.predictionTime) / float.Parse(uiManager.TimeStep));
        int offset = positions.Length - numberOfPositionsToDisplay;

        if (numberOfPositionsToDisplay > 1)//If we don't have enough positions to display, we skip everything and deactivate the overlay
        {
            Vector3[] positionsToDisplay = new Vector3[numberOfPositionsToDisplay];

            for (int i = 0; i < numberOfPositionsToDisplay; i++)
            {
                positionsToDisplay[i] = positions[i + offset];
            }

            line.positionCount = numberOfPositionsToDisplay;
            line.SetPositions(positionsToDisplay);
        }
        else
        {
            gameObject.SetActive(false);
        }


    }

}
