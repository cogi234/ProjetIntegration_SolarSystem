using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISaveContent : MonoBehaviour
{
    [SerializeField] private InputField saveNameField;
    [SerializeField] private GameObject buttonPrefab;
    private GameManager gameManager;


    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();//We find the gamemanager
    }

    private void OnEnable()
    {
        if (gameManager == null)
            gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();//We find the gamemanager

        //We get all the save names
        string[] saveNames = GameManager.GetSaveNames();

        //We destroy all existing buttons, to replace them with new buttons, in case the save files changed
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            Destroy(transform.GetChild(i).gameObject);
        }

        foreach (string name in saveNames)
        {
            //for each save, we create a button that will change the input field to its own save name
            string realName = name.Substring(Application.persistentDataPath.Length + 1, name.Length - Application.persistentDataPath.Length - 5);


            GameObject button = Instantiate(buttonPrefab, transform);
            button.GetComponent<Button>().onClick.AddListener(delegate { saveNameField.text = realName; });
            button.GetComponentInChildren<Text>().text = realName;
        }
    }
}
