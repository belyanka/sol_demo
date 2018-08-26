using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlaySceneButtons : MonoBehaviour {

	public void GoToMainMenu()
	{
		SceneManager.LoadScene("MainMenu");
	}

	public void RestartLevel() {
		LevelManagerSolipsism.Instance.RestartLevel();
	}

	public void LoadNextLevel() {
		LevelManagerSolipsism.Instance.LoadNextLevel();
	}
}
