using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIImageFlip : MonoBehaviour
{
    [SerializeField]Sprite stateTrue, stateFalse;//the two sprites we will switch between
    Image image;
    bool state = false;
    public bool State { 
        get => state; 
        set 
        {
            if (value != state)
                SwitchState();
        } 
    }


    private void Start()
    {
        image = GetComponent<Image>();
    }

    public void SwitchState()
    {
        state = !state;

        if (state)
        {
            image.sprite = stateTrue;
        }
        else
        {
            image.sprite = stateFalse;
        }

    }
}
