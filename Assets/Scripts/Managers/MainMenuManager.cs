using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
   public void Quit()
    {
        Application.Quit();
    }

    public void NewEmptySim()
    {
        GameManager.sceneStart = SceneStart.Empty;
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }
    

    public bool FlatPlane { get; set; }
    public string SunMass { get; set; }
    public string SunSize { get; set; }
    public string PlanetNumber { get; set; }
    public string MinMoonNumber { get; set; }
    public string MaxMoonNumber { get; set; }
    public void GenerateSystem()
    {
        GameManager.sceneStart = SceneStart.Generate;

        PlayerPrefs.SetInt("flatPlane", FlatPlane ? 1 : 0);
        PlayerPrefs.SetFloat("sunMass", float.Parse(SunMass));
        PlayerPrefs.SetFloat("sunSize", float.Parse(SunSize));
        PlayerPrefs.SetInt("planetNumber", int.Parse(PlanetNumber));
        PlayerPrefs.SetInt("minMoonNumber", int.Parse(MinMoonNumber));
        PlayerPrefs.SetInt("maxMoonNumber", int.Parse(MaxMoonNumber));
        PlayerPrefs.Save();

        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

    public string ObjectNumber { get; set; }
    public void FullyRandomSystem()
    {
        GameManager.sceneStart = SceneStart.FullyRandom;
        PlayerPrefs.SetInt("bodyNumber", int.Parse(ObjectNumber));
        PlayerPrefs.Save();

        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

    public void LoadSystem(string fileName)
    {
        GameManager.sceneStart = SceneStart.Load;

        PlayerPrefs.SetString("fileNameToLoad", fileName);
        PlayerPrefs.Save();

        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }
}
