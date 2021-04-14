using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIStellarObject : MonoBehaviour
{
    public StellarObject myObject;
    UIManager uiManager;
    RectTransform rectTransform;
    Text nameDisplay;
    Camera mainCamera;

    [SerializeField]bool visible = true;

    private void Start()
    {
        nameDisplay = GetComponentInChildren<Text>();
        rectTransform = GetComponent<RectTransform>();
        uiManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<UIManager>();//We find the uiManager
        mainCamera = Camera.main;

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

        //We only activate our graphics if we're visible by the main camera
        Vector3 screenPoint = Camera.main.WorldToViewportPoint(myObject.transform.position);
        if (screenPoint.x > -0.1f && screenPoint.x < 1.1f && screenPoint.y > -0.1f && screenPoint.y < 1.1f && screenPoint.z > 0)
        {
            if (!visible)
            {
                visible = true;
                GetComponent<Image>().enabled = true;
                transform.GetChild(0).gameObject.SetActive(true);
            }
        }
        else
        {
            if (visible)
            {
                visible = false;
                GetComponent<Image>().enabled = false;
                transform.GetChild(0).gameObject.SetActive(false);
            }
        }
    }
}
