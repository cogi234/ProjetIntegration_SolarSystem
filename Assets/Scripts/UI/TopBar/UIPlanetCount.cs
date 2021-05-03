using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPlanetCount : MonoBehaviour
{
    Text text;
    GameManager gameManager;

    private void Start()
    {
        text = GetComponent<Text>();
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();//We find the gameManager
    }

    private void Update()
    {
        text.text = $"Planet Count:\n {gameManager.stellarObjectList.Count}";
    }
}
