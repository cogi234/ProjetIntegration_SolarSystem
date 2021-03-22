using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


public enum SceneStart
{
    AsIs, Empty, Load, Generate
}

public class GameManager : MonoBehaviour
{
    public static SceneStart sceneStart;

    public List<StellarObject> stellarObjectList;
    public float gravityConstant;
    public float timeFactor = 1 ;
    public bool paused = false;
    public double absoluteTime = 0;

    [SerializeField] private GameObject stellarObjectUIPrefab;
    [SerializeField] private GameObject axisOverlayPrefab;
    [SerializeField] private GameObject stellarObjectPrefab;
    [SerializeField] private GameObject sunObjectPrefab;

    UIManager uiManager;
    GenerationManager generationManager;

    private void Awake()
    {
        uiManager = GetComponent<UIManager>();//We find the uiManager
        generationManager = GetComponent<GenerationManager>();//We find the generationManager
    }
    private void Start()
    {
        //We initialise the scene
        switch (sceneStart)
        {
            case SceneStart.Empty:
                ResetSolarSystem();
                break;
            case SceneStart.Load:
                LoadSolarSystem(PlayerPrefs.GetString("fileNameToLoad"));
                break;
            case SceneStart.Generate:
                ResetSolarSystem();
                generationManager.SimpleGenerateSystem(PlayerPrefs.GetInt("flatPlane") == 1, PlayerPrefs.GetFloat("sunMass"), PlayerPrefs.GetFloat("sunSize"), PlayerPrefs.GetInt("planetNumber"), PlayerPrefs.GetInt("minMoonNumber"), PlayerPrefs.GetInt("maxMoonNumber"));
                break;
            default:
                break;
        }
    }


    private void FixedUpdate()
    {
        if (!paused)//We only simulate if the game isn't paused
        {
            float realDeltaTime = Time.deltaTime * timeFactor;
            absoluteTime += realDeltaTime;

            foreach (StellarObject A in stellarObjectList)
            {
                A.ApplyGravity(realDeltaTime);
            }

            foreach (StellarObject A in stellarObjectList)
            {
                A.ApplyVelocity(realDeltaTime);
            }

            if (stellarObjectList.Count > 0)
            {
                //Centering the system:
                float X, Y, Z;
                if (uiManager.SelectedObject == null)//If there's no selected object, we center on the center of the system
                {
                    //auto adjust camera to show the whole solar system (paired with CameraMovement)
                    float xStellarObjectSum = 0;
                    float yStellarObjectSum = 0;
                    float zStellarObjectSum = 0;

                    foreach (StellarObject A in stellarObjectList)
                    {
                        xStellarObjectSum += A.transform.position.x;
                        yStellarObjectSum += A.transform.position.y;
                        zStellarObjectSum += A.transform.position.z;
                    }
                    X = xStellarObjectSum / stellarObjectList.Count;
                    Y = yStellarObjectSum / stellarObjectList.Count;
                    Z = zStellarObjectSum / stellarObjectList.Count;
                }
                else//otherwise, we center on the selected object
                {
                    X = uiManager.SelectedObject.transform.position.x;
                    Y = uiManager.SelectedObject.transform.position.y;
                    Z = uiManager.SelectedObject.transform.position.z;
                }

                foreach (StellarObject A in stellarObjectList)
                {
                    A.transform.Translate(-X, -Y, -Z, Space.World);
                }
            }
        }
    }
    
    public void CreateStellarObjectUI(StellarObject stellarObject)
    {
        //We create the UIobject and assign it a stellar object
        UIStellarObject stellarObjectUI = Instantiate(stellarObjectUIPrefab, GameObject.Find("OverlayCanvas").transform).GetComponent<UIStellarObject>();
        stellarObjectUI.myObject = stellarObject;

        //We also do the same thing for the overlay
        GameObject axisOverlay = Instantiate(axisOverlayPrefab, stellarObject.transform);
        axisOverlay.GetComponent<UIAxisOverlay>().myObject = stellarObject;
        axisOverlay.SetActive(false);
    }

    public GameObject CreateSun(string name, float mass, float density, Vector3 velocity, Vector3 position)
    {
        GameObject gObject = Instantiate(sunObjectPrefab, position, Quaternion.identity);
        StellarObject sObject = gObject.GetComponent<StellarObject>();
        gObject.name = name;
        sObject.Mass = mass;
        sObject.Density = density;
        sObject.Velocity = velocity;

        sObject.Initialise();

        return gObject;
    }
    public GameObject CreateStellarObject(string name, float mass, float density, Vector3 velocity, Vector3 position)
    {
        GameObject gObject = Instantiate(stellarObjectPrefab, position, Quaternion.identity);
        StellarObject sObject = gObject.GetComponent<StellarObject>();
        gObject.name = name;
        sObject.Mass = mass;
        sObject.Density = density;
        sObject.Velocity = velocity;

        sObject.Initialise();

        return gObject;
    }
    public static string[] GetSaveNames()
    {
        return Directory.GetFiles(Application.persistentDataPath, "*.sav");
    }

    public const string SAVE_SEPARATOR = "|\n|";
    public void SaveSolarSystem(string fileName)
    {
        Queue<string> saveContent = new Queue<string>();
        //First, we add the gameManager data
        saveContent.Enqueue(gravityConstant.ToString());
        //Then we add every stellar object
        foreach (StellarObject sObject in stellarObjectList)
        {
            sObject.Save(saveContent);
        }

        //Then we join everything into a string to save into the file
        string saveString = string.Join(SAVE_SEPARATOR, saveContent);
        File.WriteAllText($"{Application.persistentDataPath}/{fileName}.sav", saveString);
    }

    public void LoadSolarSystem(string fileName)
    {
        //We reset the field
        ResetSolarSystem();        

        //we take the file
        string saveString = File.ReadAllText($"{Application.persistentDataPath}/{fileName}.sav");
        //we split the string into all the separate values and put it into a queue
        Queue<string> saveContent = new Queue<string>(saveString.Split(SAVE_SEPARATOR.ToCharArray(), StringSplitOptions.RemoveEmptyEntries));

        //We load the gameManager data
        gravityConstant = float.Parse(saveContent.Dequeue());


        //As long as some data remains, we continue to create stellarObjects
        while(saveContent.Count > 0)
        {
            string name = saveContent.Dequeue();
            float mass = float.Parse(saveContent.Dequeue());
            float density = float.Parse(saveContent.Dequeue());
            Vector3 velocity = new Vector3(float.Parse(saveContent.Dequeue()), float.Parse(saveContent.Dequeue()), float.Parse(saveContent.Dequeue()));
            Vector3 position = new Vector3(float.Parse(saveContent.Dequeue()), float.Parse(saveContent.Dequeue()), float.Parse(saveContent.Dequeue()));

            //If the name starts with Sun, we create a sun, otherwise it is a normal stellar object like a planet or a moon.
            if (name.StartsWith("Sun"))
            {

                CreateSun(name, mass, density, velocity, position);
            } 
            else
            {
                CreateStellarObject(name, mass, density, velocity, position);
            }
        }
    }

    private void ResetSolarSystem()//This is a function to reset the solar system to empty.
    {
        for (int i = GameObject.Find("OverlayCanvas").transform.childCount - 1; i >= 0; i--)//This deletes all overlays associated with stellar objects
        {
            Destroy(GameObject.Find("OverlayCanvas").transform.GetChild(i).gameObject);
        }

        for (int i = stellarObjectList.Count - 1; i >= 0; i--)
        {
            Destroy(stellarObjectList[i].gameObject);
            stellarObjectList.RemoveAt(i);
        }
    }
}
