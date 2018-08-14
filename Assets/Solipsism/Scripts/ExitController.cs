using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitController : MonoBehaviour {

	public GameController gameController;

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.name == "Player")
		{
			gameController.LevelFinished();
            
			Debug.Log("LVL COMPLETE");
		}
	}
}
