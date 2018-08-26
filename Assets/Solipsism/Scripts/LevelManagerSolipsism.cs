using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManagerSolipsism : MonoBehaviour {
    
    private static LevelManagerSolipsism instance;
    private Dictionary<int, string> levels;

    public static LevelManagerSolipsism Instance
    {
        get {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<LevelManagerSolipsism>();
             
                if (instance == null)
                {
                    GameObject container = new GameObject("LevelManagerSolipsism");
                    instance = container.AddComponent<LevelManagerSolipsism>();
                }
            }
     
            return instance;
        }
    }
    
    void Awake() {

        DontDestroyOnLoad(this.gameObject);
        LoadLevelMap();
    }

    private void LoadLevelMap() {
        int sceneCount = SceneManager.sceneCountInBuildSettings;
        levels = new Dictionary<int, string>();
        for (int i = 1; i < sceneCount; i++) {
            levels.Add(i, SceneUtility.GetScenePathByBuildIndex(i));
        }
    }

    public void LoadLevelNumber(int number) {
        SceneManager.LoadScene(number);
    }

    public void RestartLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadNextLevel() {
        if (SceneManager.GetActiveScene().buildIndex == levels.Count) {
            SceneManager.LoadScene("MainMenu");
        }
        else {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    public Dictionary<int, string> GetLevelsList() {
        return levels;
    }
}