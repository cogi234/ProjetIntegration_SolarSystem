using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITimeScript : MonoBehaviour
{
    private GameManager gameManager;
    private Text textBox;

    private void Start()
    {
        textBox = GetComponent<Text>();
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }

    private void Update()
    {
        //Il va falloir attendre que Zach finisse le script de gameManager pour completer
        textBox.text = $"Time: \n {gameManager.time}";
    }
}
