using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMessage : MonoBehaviour
{
    private double maxTime, counter;
    Image image;
    Text text;

    public void Initialise(string message, double time)
    {
        image = GetComponent<Image>();
        text = transform.GetChild(0).GetComponent<Text>();

        text.text = message;

        maxTime = time;
        counter = time;
    }

    private void Update()
    {
        //We fade the image over time
        image.color = new Color(image.color.r, image.color.g, image.color.b, (float)(counter / maxTime));
        text.color = new Color(text.color.r, text.color.g, text.color.b, (float)(counter / maxTime));

        //We count down the timer
        counter -= Time.deltaTime;

        if (counter <= 0)
        {
            Destroy(gameObject);
        }
    }
}
