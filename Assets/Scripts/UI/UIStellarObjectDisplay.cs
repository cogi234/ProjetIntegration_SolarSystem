using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class UIStellarObjectDisplay : MonoBehaviour
{
    public StellarObject selectedObject;
    Text infoText;

    private void Start()
    {
        infoText = GetComponent<Text>();
    }

    private void Update()//We'll have to put the text once we've finished teh stellarobject script
    {
        infoText.text = $"<b>{selectedObject.name}</b>\n Mass: {selectedObject.Mass}\n Density: {selectedObject.Density}\n Volume: {selectedObject.Volume}\n Radius: {selectedObject.Radius}\n Speed: {selectedObject.Velocity.magnitude}";
    }

}
