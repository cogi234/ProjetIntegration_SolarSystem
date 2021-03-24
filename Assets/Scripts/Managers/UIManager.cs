using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject axisOverlayPrefab;

    private StellarObject selectedObject;
    public StellarObject SelectedObject { 
        get
        {
            return selectedObject;
        }
        set
        {
            if (selectedObject != null)
                selectedObject.transform.GetChild(0).gameObject.SetActive(false);//We deactivate the axis overlay of the previous selected object
            selectedObject = value;
            selectedObject.transform.GetChild(0).gameObject.SetActive(true);//We activate the axis overlay of the next selected object
        }
    }
}
