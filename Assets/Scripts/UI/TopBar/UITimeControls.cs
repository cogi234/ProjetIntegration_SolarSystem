using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class UITimeControls : MonoBehaviour
{
    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }

    //Pause controls:
    float previousTimeScale = 0f;
    bool paused = false;

    public void TogglePause()
    {
        paused = !paused;
        float temp = previousTimeScale;
        previousTimeScale = gameManager.timeFactor;
        gameManager.timeFactor = temp;
    }


    //Time Scale:
    public string TimeScale { get; set; }

    public void ApplyTimeScale()
    {
        if (paused)
        {
            previousTimeScale = float.Parse(TimeScale);
        }
        else
        {
            gameManager.timeFactor = float.Parse(TimeScale);
        }
    }

}
