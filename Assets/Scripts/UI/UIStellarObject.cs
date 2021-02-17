using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;


public class UIStellarObject : MonoBehaviour
{
    public StellarObject myObject;
    UIStellarObjectDisplay display;
    RectTransform rectTransform;
    Text nameDisplay;

    private void Start()
    {
        nameDisplay = GetComponentInChildren<Text>();
        rectTransform = GetComponent<RectTransform>();
        display = GameObject.Find("StellarObjectDisplay").GetComponent<UIStellarObjectDisplay>();//Il ne faut pas oublier de bien nommer l'objet

        nameDisplay.text = myObject.name;
    }

    public void SelectedObject()
    {
        display.selectedObject = myObject;
    }

    private void Update()
    {
        //We make the icon follow its stellar object
        Vector2 viewportPos = Camera.main.WorldToScreenPoint(myObject.transform.position);
        rectTransform.anchoredPosition = viewportPos;
    }
}
