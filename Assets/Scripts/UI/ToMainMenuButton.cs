using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToMainMenuButton : MonoBehaviour {

	public void GoToMainMenu()
	{
		SceneManager.LoadScene("MainMenu");
	}
}
