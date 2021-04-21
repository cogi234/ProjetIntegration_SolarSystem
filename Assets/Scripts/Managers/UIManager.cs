using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    private StellarObject selectedObject;
    public StellarObject SelectedObject { 
        get
        {
            return selectedObject;
        }
        set
        {
            if (selectedObject != null)
                selectedObject.transform.GetChild(0).gameObject.SetActive(false);//We deactivate the axis overlay of the previous selected object
            selectedObject = value;
            selectedObject.transform.GetChild(0).gameObject.SetActive(true);//We activate the axis overlay of the next selected object
        }
    }



    GameManager gameManager;
    SimulationManager simulationManager;
    void Start()
    {
        gameManager = GetComponent<GameManager>();
        simulationManager = GetComponent<SimulationManager>();
    }

    public void CollisionMode(int mode)
    {
        gameManager.collisionMode = (GameManager.CollisionMode)mode;
    }

    public bool Centering {
        get
        {
            return gameManager.centering;
        }
        set
        {
            gameManager.centering = value;
        }
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }


    //Prediction stuff:
    public string TimeStep { get; set; } = "0.1";
    public string StepCount { get; set; } = "2000";
    public bool AllPlanets { get; set; } = true;
    public double predictionTime;//The time at which we predicted the current trajectory

    public void Predict()
    {
        predictionTime = gameManager.absoluteTime;//We record the time at which we predicted the future

        simulationManager.Simulate(float.Parse(TimeStep), int.Parse(StepCount));//We simulate the future

        if (AllPlanets)
        {
            //We activate every prediction overlay and then set their position lists
            foreach (StellarObject A in gameManager.stellarObjectList)
            {
                A.transform.GetChild(1).gameObject.SetActive(true);

                A.transform.GetChild(1).GetComponent<UIPredictionOverlay>().positions = simulationManager.GetPositionHistoryOfObject(gameManager.stellarObjectList.IndexOf(A));
            }
        }
        else
        {
            //We deactivate every prediction overlay, except the selected object, before setting its position list
            foreach (StellarObject A in gameManager.stellarObjectList)
            {
                A.transform.GetChild(1).gameObject.SetActive(false);
            }
            selectedObject.transform.GetChild(1).gameObject.SetActive(true);

            selectedObject.transform.GetChild(1).GetComponent<UIPredictionOverlay>().positions = simulationManager.GetPositionHistoryOfObject(gameManager.stellarObjectList.IndexOf(selectedObject));
        }
    }

    public void JumpTime()
    {
        gameManager.JumpTime(float.Parse(TimeStep), int.Parse(StepCount));
    }
}
