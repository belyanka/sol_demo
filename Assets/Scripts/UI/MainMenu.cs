using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public RectTransform panel;
    public GameObject buttonPrefab;


    // Use this for initialization
    void Start()
    {
        EditorBuildSettingsScene[] scenes = EditorBuildSettings.scenes;

        for (int i = 1; i < scenes.Length; i++)
        {
            GameObject newButton = Instantiate(buttonPrefab);
            newButton.transform.SetParent(panel);
            newButton.GetComponent<ButtonScript>().SceneNumber = i;
            newButton.GetComponentInChildren<Text>().text = scenes[i].path;
        }
    }

    public void QuitGame()
    {
        // save any game data here
#if UNITY_EDITOR
        // Application.Quit() does not work in the editor so
        // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
        UnityEditor.EditorApplication.isPlaying = false;
#else
         	Application.Quit();
     	#endif
    }
}