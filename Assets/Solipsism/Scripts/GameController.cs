using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
	[SerializeField]
	private GameObject winPanel;

	[SerializeField] 
	private GameObject losePanel;
	
	// Use this for initialization
	void Start ()
	{
		Time.timeScale = 1;
		winPanel.SetActive(false);
		losePanel.SetActive(false);
	}

	public void LevelFinished()
	{
		Time.timeScale = 0;
		winPanel.SetActive(true);
	}

	public void GameOver()
	{
		Time.timeScale = 0;
		losePanel.SetActive(true);
	}
}
