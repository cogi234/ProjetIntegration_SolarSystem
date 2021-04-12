using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class UIStellarObjectDisplay : MonoBehaviour
{
    UIManager uiManager;

    [SerializeField] InputField nameInput;
    [SerializeField] InputField massInput;
    [SerializeField] InputField densityInput;
    [SerializeField] InputField volumeInput;

    [SerializeField] InputField speedInput;
    [SerializeField] InputField velocityXInput;
    [SerializeField] InputField velocityYInput;
    [SerializeField] InputField velocityZInput;

    [SerializeField] InputField positionXInput;
    [SerializeField] InputField positionYInput;
    [SerializeField] InputField positionZInput;


    private void Start()
    {
        uiManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<UIManager>();//We find the uiManager
    }

    private void Update()
    {
        if (uiManager.SelectedObject != null)
        {
            if (!nameInput.isFocused)
                nameInput.text = $"{uiManager.SelectedObject.name}";
            if (!massInput.isFocused)
                massInput.text = uiManager.SelectedObject.Mass.ToString();
            if (!densityInput.isFocused)
                densityInput.text = uiManager.SelectedObject.Density.ToString();
            if (!volumeInput.isFocused)
                volumeInput.text = uiManager.SelectedObject.Volume.ToString();
            if (!speedInput.isFocused)
                speedInput.text = uiManager.SelectedObject.Velocity.magnitude.ToString();
            if(!(velocityXInput.isFocused || velocityYInput.isFocused || velocityZInput.isFocused))
            {
                velocityXInput.text = uiManager.SelectedObject.Velocity.normalized.x.ToString();
                velocityYInput.text = uiManager.SelectedObject.Velocity.normalized.y.ToString();
                velocityZInput.text = uiManager.SelectedObject.Velocity.normalized.z.ToString();
            }
            if(!(positionXInput.isFocused || positionYInput.isFocused || positionZInput.isFocused))
            {
                positionXInput.text = uiManager.SelectedObject.transform.position.x.ToString();
                positionYInput.text = uiManager.SelectedObject.transform.position.y.ToString();
                positionZInput.text = uiManager.SelectedObject.transform.position.z.ToString();
            }
        }
        else
        {
            nameInput.text = "No Selected Object";
        }

    }

    public void UpdateName()
    {
        uiManager.SelectedObject.gameObject.name = nameInput.text;
    }
    public void UpdateMass()
    {
        uiManager.SelectedObject.Mass = float.Parse(massInput.text);
    }
    public void UpdateDensity()
    {
        uiManager.SelectedObject.Density = float.Parse(densityInput.text);
    }
    public void UpdateVolume()
    {
        uiManager.SelectedObject.Volume = float.Parse(volumeInput.text);
    }
    public void UpdateSpeed()
    {
        if (uiManager.SelectedObject.Velocity.sqrMagnitude == 0)
            uiManager.SelectedObject.Velocity = float.Parse(speedInput.text) * Vector3.one.normalized;
        else
            uiManager.SelectedObject.Velocity = float.Parse(speedInput.text) * uiManager.SelectedObject.Velocity.normalized;
    }
    public void UpdateVelocity()
    {
        if (uiManager.SelectedObject.Velocity.sqrMagnitude == 0)
            uiManager.SelectedObject.Velocity = 0.01f * new Vector3(float.Parse(velocityXInput.text), float.Parse(velocityYInput.text), float.Parse(velocityZInput.text)).normalized;
        else
            uiManager.SelectedObject.Velocity = uiManager.SelectedObject.Velocity.magnitude * new Vector3(float.Parse(velocityXInput.text), float.Parse(velocityYInput.text), float.Parse(velocityZInput.text)).normalized;
    }
    public void UpdatePosition()
    {
        uiManager.SelectedObject.transform.position = new Vector3(float.Parse(positionXInput.text), float.Parse(positionYInput.text), float.Parse(positionZInput.text));
    }

}
