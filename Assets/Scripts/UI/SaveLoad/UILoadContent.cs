using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UILoadContent : MonoBehaviour
{
    [SerializeField] private GameObject buttonPrefab;
    private GameManager gameManager;
    private MainMenuManager mainMenuManager;


    private void Awake()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
            gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();//We find the gamemanager
        else
            mainMenuManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<MainMenuManager>();
    }

    private void OnEnable()
    {
        if (gameManager == null && SceneManager.GetActiveScene().buildIndex == 1)
            gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();//We find the gamemanager
        else if (mainMenuManager == null && SceneManager.GetActiveScene().buildIndex == 0)
            mainMenuManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<MainMenuManager>();

        //We get all the save names
        string[] saveNames = GameManager.GetSaveNames();

        //We destroy all existing buttons, to replace them with new buttons, in case the save files changed
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            Destroy(transform.GetChild(i).gameObject);
        }

        foreach (string name in saveNames)
        {
            //for each save, we create a button that will load its associated save
            string realName = name.Substring(Application.persistentDataPath.Length + 1, name.Length - Application.persistentDataPath.Length - 5);

            GameObject button = Instantiate(buttonPrefab, transform);

            if (SceneManager.GetActiveScene().buildIndex == 1)
                button.GetComponent<Button>().onClick.AddListener(delegate { gameManager.LoadSolarSystem(realName); });
            else if (SceneManager.GetActiveScene().buildIndex == 0)
                button.GetComponent<Button>().onClick.AddListener(delegate { mainMenuManager.LoadSystem(realName); });

            button.GetComponentInChildren<Text>().text = realName;
        }
    }
}
