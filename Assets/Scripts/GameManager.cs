using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<StellarObject> stellarObjectList;
    public float gravityConstant;

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
}
