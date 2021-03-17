using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private StellarObject selectedObject;
    public StellarObject SelectedObject { 
        get
        {
            return selectedObject;
        }
        set
        {
            selectedObject = value;
        }
    }


}
