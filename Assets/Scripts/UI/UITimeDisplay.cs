using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UITimeDisplay : MonoBehaviour
{
    private Text textBox;
    private GameManager gameManager;

    private void Start()
    {
        textBox = GetComponent<Text>();

        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }

    private void Update()
    {
        //Comme PlaceHolder, je met le temps de Time. On va changer ca avec les outils de controle de temps
        double time = gameManager.absoluteTime;
        int year = (int)Math.Floor(time / 31536000);//31536000 secondes par an
        time -= year * 31536000;
        int day = (int)Math.Floor(time / 86400);//86400 secondes par jour
        time -= day * 86400;
        int hour = (int)Math.Floor(time / 3600);//3600 secondes par heure
        time -= hour * 3600;
        int minute = (int)Math.Floor(time / 60);//60secondes par minute
        time -= minute * 60;

        textBox.text = $"Time: \n {year}y {day}d {hour}h {minute}m {(int)Math.Floor(time)}s";
    }
}
