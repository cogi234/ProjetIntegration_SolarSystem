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
        if (uiManager.SelectedObject != null)
        {
            infoText.text = $"<b>{uiManager.SelectedObject.name}</b>\n Mass: {uiManager.SelectedObject.Mass}\n Density: {uiManager.SelectedObject.Density}\n Volume: {uiManager.SelectedObject.Volume}\n Radius: {uiManager.SelectedObject.Radius}\n Speed: {uiManager.SelectedObject.Velocity.magnitude}";
        }
        else
        {
            infoText.text = "<b>No Selected Object</b>";
        }

    }

}
