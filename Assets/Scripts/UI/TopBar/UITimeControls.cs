using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class UITimeControls : MonoBehaviour
{
    private GameManager gameManager;
    [SerializeField] UIImageFlip pauseButtonGraphics;

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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TogglePause();
            pauseButtonGraphics.SwitchState();
        }
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
