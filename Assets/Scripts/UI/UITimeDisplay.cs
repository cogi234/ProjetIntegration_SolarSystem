using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITimeDisplay : MonoBehaviour
{
    private Text textBox;

    private void Start()
    {
        textBox = GetComponent<Text>();
    }

    private void Update()
    {
        //Comme PlaceHolder, je met le temps de Time. On va changer ca avec les outils de controle de temps
        float time = Time.time
            ;
        int year = Mathf.FloorToInt(time / 31536000);//31536000 secondes par an
        time -= year * 31536000;
        int day = Mathf.FloorToInt(time / 86400);//86400 secondes par jour
        time -= day * 86400;
        int hour = Mathf.FloorToInt(time / 3600);//3600 secondes par heure
        time -= hour * 3600;
        int minute = Mathf.FloorToInt(time / 60);//60secondes par minute
        time -= minute * 60;

        textBox.text = $"Time: \n {year}y {day}d {hour}h {minute}m {Mathf.FloorToInt(time)}s";
    }
}
