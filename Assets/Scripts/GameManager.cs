using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<StellarObject> stellarObjectList;
    public float gravityConstant;

    [SerializeField]private GameObject stellarObjectUIPrefab;

    private void FixedUpdate()
    {
        foreach(StellarObject X in stellarObjectList) 
        {
            X.ApplyGravity(Time.fixedDeltaTime);
        }

        foreach(StellarObject X in stellarObjectList) 
        {
            X.ApplyVelocity(Time.fixedDeltaTime);
        }
    }

    public void CreateStellarObjectUI(StellarObject stellarObject)
    {
        //We create the UIobject and assign it a stellar object
        UIStellarObject stellarObjectUI = Instantiate(stellarObjectUIPrefab, GameObject.Find("OverlayCanvas").transform).GetComponent<UIStellarObject>();
        stellarObjectUI.myObject = stellarObject;
    }
}
