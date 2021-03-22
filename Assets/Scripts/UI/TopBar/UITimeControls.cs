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
    public void TogglePause()
    {
        gameManager.paused = !gameManager.paused;
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
        gameManager.timeFactor = float.Parse(TimeScale);
    }

}
