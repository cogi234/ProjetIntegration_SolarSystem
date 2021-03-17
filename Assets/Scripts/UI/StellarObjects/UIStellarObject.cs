using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;


public class UIStellarObject : MonoBehaviour
{
    public StellarObject myObject;
    UIManager uiManager;
    RectTransform rectTransform;
    Text nameDisplay;

    private void Start()
    {
        nameDisplay = GetComponentInChildren<Text>();
        rectTransform = GetComponent<RectTransform>();
        uiManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<UIManager>();//We find the uiManager

        nameDisplay.text = myObject.name;
    }

    public void SelectedObject()
    {
        uiManager.SelectedObject = myObject;
    }

    private void Update()
    {
        //We make the icon follow its stellar object
        Vector2 viewportPos = Camera.main.WorldToScreenPoint(myObject.transform.position);
        rectTransform.anchoredPosition = viewportPos;

        //We update the name on the display
        nameDisplay.text = myObject.name;
    }
}
