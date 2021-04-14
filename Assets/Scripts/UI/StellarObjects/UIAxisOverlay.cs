using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAxisOverlay : MonoBehaviour
{
    private LineRenderer velocityLine, xLine, yLine, zLine;
    public StellarObject myObject;

    private void Awake()
    {
        xLine = transform.GetChild(0).GetComponent<LineRenderer>();
        yLine = transform.GetChild(1).GetComponent<LineRenderer>();
        zLine = transform.GetChild(2).GetComponent<LineRenderer>();
        velocityLine = transform.GetChild(3).GetComponent<LineRenderer>();
    }

    private void Update()
    {
        xLine.SetPosition(1, new Vector3(1.5f, 0, 0));
        yLine.SetPosition(1, new Vector3(0, 1.5f, 0));
        zLine.SetPosition(1, new Vector3(0, 0, 1.5f));
        velocityLine.SetPosition(1, myObject.Velocity.normalized * 1.5f);
    }
}
