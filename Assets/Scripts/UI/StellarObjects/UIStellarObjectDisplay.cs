using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class UIStellarObjectDisplay : MonoBehaviour
{
    UIManager uiManager;
    Text infoText;

    private void Start()
    {
        infoText = GetComponent<Text>();
        uiManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<UIManager>();//We find the uiManager
    }

    private void Update()//We'll have to put the text once we've finished teh stellarobject script
    {
        if (uiManager.selectedObject != null)
        {
            infoText.text = $"<b>{uiManager.selectedObject.name}</b>\n Mass: {uiManager.selectedObject.Mass}\n Density: {uiManager.selectedObject.Density}\n Volume: {uiManager.selectedObject.Volume}\n Radius: {uiManager.selectedObject.Radius}\n Speed: {uiManager.selectedObject.Velocity.magnitude}";
        }
        else
        {
            infoText.text = "<b>No Selected Object</b>";
        }

    }

}
