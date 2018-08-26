﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public RectTransform panel;
    public GameObject buttonPrefab;
    private LevelManagerSolipsism manager;

    // Use this for initialization
    void Start() {

        Dictionary<int, string> levels = LevelManagerSolipsism.Instance.GetLevelsList();

        foreach (var level in levels) {
            
            GameObject newButton = Instantiate(buttonPrefab);
            newButton.transform.SetParent(panel,false);
            newButton.GetComponent<ButtonScript>().SceneNumber = level.Key;
            newButton.GetComponentInChildren<Text>().text = level.Value;
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