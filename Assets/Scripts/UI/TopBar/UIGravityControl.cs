using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGravityControl : MonoBehaviour
{
    private GameManager gameManager;
    [SerializeField] InputField gravityInput;

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }


    private void Update()
    {
        if (!gravityInput.isFocused)
            gravityInput.text = gameManager.gravityConstant.ToString();
    }

    public void ApplyGravityConstant()
    {
        gameManager.gravityConstant = float.Parse(gravityInput.text);
    }
}
