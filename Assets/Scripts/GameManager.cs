using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<StellarObject> stellarObjectList;
    public string saveName = "";
    public float gravityConstant;

    [SerializeField] private GameObject stellarObjectUIPrefab;
    [SerializeField] private GameObject stellarObjectPrefab;
    [SerializeField] private GameObject sunObjectPrefab;


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

    public GameObject CreateSun(string name, float mass, float density, float volume, Vector3 velocity, Vector3 position)
    {
        GameObject gObject = Instantiate(sunObjectPrefab, position, Quaternion.identity);
        StellarObject sObject = gObject.GetComponent<StellarObject>();
        gObject.name = name;
        sObject.Mass = mass;
        sObject.Density = density;
        sObject.Volume = volume;
        sObject.Velocity = velocity;

        return gObject;
    }
    public GameObject CreateStellarObject(string name, float mass, float density, float volume, Vector3 velocity, Vector3 position)
    {
        GameObject gObject = Instantiate(sunObjectPrefab, position, Quaternion.identity);
        StellarObject sObject = gObject.GetComponent<StellarObject>();
        gObject.name = name;
        sObject.Mass = mass;
        sObject.Density = density;
        sObject.Volume = volume;
        sObject.Velocity = velocity;

        return gObject;
    }

    private void Start()
    {
        Debug.Log(Application.persistentDataPath);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            SaveSolarSystem("test");
        }
        else if (Input.GetKeyDown(KeyCode.L))
        {
            LoadSolarSystem("test");
        }
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
        File.WriteAllText($"{Application.persistentDataPath}/{fileName}.txt", saveString);

        Debug.Log(saveString);
    }

    public void LoadSolarSystem(string fileName)
    {
        //We reset the field
        for (int i = GameObject.Find("OverlayCanvas").transform.childCount - 1; i >= 0; i--)
        {
            Destroy(GameObject.Find("OverlayCanvas").transform.GetChild(i).gameObject);
        }
        for (int i = stellarObjectList.Count - 1; i >= 0; i--)
        {
            Destroy(stellarObjectList[i]);
            stellarObjectList.RemoveAt(i);
        }
        

        //we take the file
        string saveString = File.ReadAllText($"{Application.persistentDataPath}/{fileName}.txt");
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
            float volume = float.Parse(saveContent.Dequeue());
            Vector3 velocity = new Vector3(float.Parse(saveContent.Dequeue()), float.Parse(saveContent.Dequeue()), float.Parse(saveContent.Dequeue()));
            Vector3 position = new Vector3(float.Parse(saveContent.Dequeue()), float.Parse(saveContent.Dequeue()), float.Parse(saveContent.Dequeue()));

            if (name.StartsWith("Sun"))
            {
                CreateSun(name, mass, density, volume, velocity, position);
            } else
            {
                CreateStellarObject(name, mass, density, volume, velocity, position);
            }
        }
    }
}
