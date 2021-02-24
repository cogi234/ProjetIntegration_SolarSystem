using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISaveController : MonoBehaviour
{
    [SerializeField] private InputField saveNameInput;
    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();//We find the gamemanager
    }

    public void Save()
    {
        gameManager.SaveSolarSystem(saveNameInput.text);
    }
}
