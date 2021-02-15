using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;


[RequireComponent(typeof(StellarObject))]//We need a stellar object to work
public class UIStellarObject : MonoBehaviour
{
    StellarObject myObject;
    UIStellarObjectDisplay display;

    private void Start()
    {
        myObject = GetComponent<StellarObject>();
        display = GameObject.Find("StellarObjectDisplay").GetComponent<UIStellarObjectDisplay>();//Il ne faut pas oublier de bien nommer l'objet
    }

    private void OnMouseDown()
    {
        display.selectedObject = myObject;
    }
}
