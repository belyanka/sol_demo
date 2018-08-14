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
		LevelManager.Instance.RestartLevel();
	}

	public void LoadNextLevel() {
		LevelManager.Instance.LoadNextLevel();
	}
}
